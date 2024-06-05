using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace StroyCompany
{
    public partial class MainWindow : Window
    {
        private DataBase database;

        public MainWindow()
        {
            InitializeComponent();
            database = new DataBase();
            LoadData();
        }

        private void LoadData()
        {
            string selectQuery = @"
            SELECT Объект.*, Рабочая_группа.Название AS Рабочая_группа 
            FROM Объект 
            JOIN Рабочая_группа ON Объект.FK_Рабочая_группа = Рабочая_группа.id_Рабочая_группа";

            database.openConnection();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, database.sqlConnection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            objectsDataGrid.ItemsSource = dataTable.DefaultView;
            database.closeConnection();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddObjectWindow addObjectWindow = new AddObjectWindow();
            addObjectWindow.ShowDialog();
            LoadData();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (objectsDataGrid.SelectedItem is DataRowView selectedRow)
            {
                EditObjectWindow editObjectWindow = new EditObjectWindow(selectedRow);
                editObjectWindow.ShowDialog();
                LoadData();
            }
            else
            {
                MessageBox.Show("Please select an object to edit.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (objectsDataGrid.SelectedItem is DataRowView selectedRow)
            {
                string name = selectedRow["Название"].ToString();

                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить объект \"{name}\"?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    string deleteQuery = "DELETE FROM Объект WHERE Название = @name";

                    database.openConnection();
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, database.sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@name", name);

                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Ошибка при удалении объекта: " + ex.Message);
                        }
                    }
                    database.closeConnection();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите объект для удаления.");
            }
        }
    }
}
