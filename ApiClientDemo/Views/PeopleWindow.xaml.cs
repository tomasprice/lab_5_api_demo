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
    public partial class PeopleWindow : Window
    {
        private ApiHandler _apiHandler;
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
            var personCountPrevious = _apiHandler.Get<Person>().Count;

            if (!_apiHandler.Add(person, CheckTownRestrict.IsChecked == true))
            {
                return;
            }

            var personCountCurrent = _apiHandler.Get<Person>().Count;
            if (personCountPrevious == personCountCurrent)
            {
                MessageBox.Show("Town restriction is on. Person hasn't been added!", "App", MessageBoxButton.OK, MessageBoxImage.Information);

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

            if (!_apiHandler.Update(person, currentPersonId))
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

            if (!_apiHandler.Delete(currentPersonId))
            {
                return;
            }

            MessageBox.Show("Person has been deleted!", "App", MessageBoxButton.OK, MessageBoxImage.Information);
            GetData();

            currentPersonId = -1;
        }

        private void FilterButton(object sender, RoutedEventArgs e)
        {
            this.PeopleList.Items.Clear();

            var birthYear = 0;
            int.TryParse(TextBirthFilter.Text, out birthYear);

            var people = _apiHandler.Get<Person>(
                CheckMatchCaseFilter.IsChecked == true,
                CheckCaseSensFilter.IsChecked == true,
                PersonName.Text,
                birthYear,
                PersonTown.Text);

            if (people == null || people.Count <= 0)
            {
                return;
            }

            foreach (var person in people)
            {
                this.PeopleList.Items.Add(person);
            }
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
                BirthYear = PersonBirthYear.SelectedDate.GetValueOrDefault(),
                Town = PersonTown.Text
            };
        }

        private void PopulateTextFields(Person person)
        {
            PersonName.Text = person.Name;
            PersonSurname.Text = person.Surname;
            PersonBirthYear.SelectedDate = person.BirthYear;
            PersonTown.Text = person.Town;

            currentPersonId = person.Id;
        }

        private void CreateApiHandler()
        {
            var baseAddress = "http://localhost:55064/person/";
            _apiHandler = new ApiHandler(baseAddress);
        }

        private void GetData()
        {
            this.PeopleList.Items.Clear();

            var people = _apiHandler.Get<Person>();

            if (people == null || people.Count <= 0)
            {
                return;
            }
          
            foreach (var person in people)
            {
                this.PeopleList.Items.Add(person);
            }
        }
   
    }
}
