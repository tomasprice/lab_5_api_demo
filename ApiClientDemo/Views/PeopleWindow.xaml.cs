using ApiClientDemo.API;
using restApiDemo.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ApiClientDemo.Views
{
    /// <summary>
    /// Interaction logic for PeopleWindow.xaml
    /// </summary>
    public partial class PeopleWindow : Window
    {
        private ApiHandler apiHandler;
        private int currentPersonId = -1;

        public PeopleWindow()
        {
            InitializeComponent();
            CreateApiHandler();
            GetData();
        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void AddPerson(object sender, RoutedEventArgs e)
        {
            var person = GetPerson();

            if (!apiHandler.Add(person))
            {
                return;
            }

            MessageBox.Show("Person has been added!", "App", MessageBoxButton.OK, MessageBoxImage.Information);
            GetData();
        }

        private void UpdatePerson(object sender, RoutedEventArgs e)
        {
            if (currentPersonId == -1)
            {
                MessageBox.Show("Select person first!", "App", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var person = GetPerson();

            if (!apiHandler.Update(person, currentPersonId))
            {
                return;
            }

            MessageBox.Show("Person has been updated!", "App", MessageBoxButton.OK, MessageBoxImage.Information);
            GetData();

            currentPersonId = -1;
        }

        private void DeletePerson(object sender, RoutedEventArgs e)
        {
            if (currentPersonId == -1)
            {
                MessageBox.Show("Select person first!", "App", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!apiHandler.Delete(currentPersonId))
            {
                return;
            }

            MessageBox.Show("Person has been deleted!", "App", MessageBoxButton.OK, MessageBoxImage.Information);
            GetData();

            currentPersonId = -1;
        }

        private void PeopleList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedPerson = PeopleList.SelectedItem;
            if (selectedPerson == null)
            {
                return;
            }

            PopulateTextFields((Person)selectedPerson);
        }

        private Person GetPerson()
        {
           return new Person
           {
                Name = PersonName.Text,
                Surname = PersonSurname.Text,
                BirthYear = Convert.ToDateTime(PersonBirthYear.Text),
                Town = PersonTown.Text
            };
        }


        private void PopulateTextFields(Person person)
        {
            PersonName.Text = person.Name;
            PersonSurname.Text = person.Surname;
            PersonBirthYear.Text = person.BirthYear.ToLongDateString();
            PersonTown.Text = person.Town;

            currentPersonId = person.Id;
        }

        private void CreateApiHandler()
        {
            var baseAddress = "http://localhost:55064/person/";
            apiHandler = new ApiHandler(baseAddress);
        }

        private void GetData()
        {
            List<Person> people = apiHandler.Get<Person>();

            if (people == null || people.Count <= 0)
            {
                return;
            }

            this.PeopleList.Items.Clear();

            foreach (var person in people)
            {
                this.PeopleList.Items.Add(person);
            }
        }

    }
}
