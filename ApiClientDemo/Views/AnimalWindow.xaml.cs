using ApiClientDemo.API;
using restApiDemo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ApiClientDemo.Views
{
    public partial class AnimalWindow : Window
    {
        private List<DateTime> captureDates = new List<DateTime>();
        private List<string> occurences = new List<string>();
        private ApiHandler _apiHandler;
        private int currentAnimalId = -1;

        public AnimalWindow()
        {
            InitializeComponent();
            CreateApiHandler();
            GetData();
        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void AddAnimal(object sender, RoutedEventArgs e)
        {
            var animal = GetAnimal();

            if (!_apiHandler.Add(animal))
            {
                return;
            }

            MessageBox.Show("Animal has been added!", "App", MessageBoxButton.OK, MessageBoxImage.Information);
            GetData();
        }

        private void UpdateAnimal(object sender, RoutedEventArgs e)
        {
            if (currentAnimalId == -1)
            {
                MessageBox.Show("Select animal first!", "App", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var animal = GetAnimal();

            if (!_apiHandler.Update(animal, currentAnimalId))
            {
                return;
            }

            MessageBox.Show("Animal has been updated!", "App", MessageBoxButton.OK, MessageBoxImage.Information);
            GetData();

            currentAnimalId = -1;
        }

        private void DeleteAnimal(object sender, RoutedEventArgs e)
        {
            if (currentAnimalId == -1)
            {
                MessageBox.Show("Select animal first!", "App", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!_apiHandler.Delete(currentAnimalId))
            {
                return;
            }

            MessageBox.Show("Animal has been deleted!", "App", MessageBoxButton.OK, MessageBoxImage.Information);
            GetData();

            currentAnimalId = -1;
        }

        private void AnimalsList_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var selectedAnimal = AnimalList.SelectedItem;
            if (selectedAnimal == null)
            {
                return;
            }

            PopulateTextFields((Animal)selectedAnimal);
        }

        private object GetAnimal()
        {
            return new Animal
            {
                Name = AnimalName.Text,
                Species = AnimalSpecies.Text,
                Weight = Convert.ToInt16(AnimalWeight.Text),
                Carnivorous = GetCarnivorous(),
                CaptureDates = captureDates,
                PlacesOfOccurrence = occurences
            };
        }

        private bool GetCarnivorous()
        {
            if (CarnivorousTrue.IsChecked == true)
            {
                return true;
            }
            return false;
        }

        private void PopulateTextFields(Animal selectedAnimal)
        {
            AnimalName.Text = selectedAnimal.Name;
            AnimalSpecies.Text = selectedAnimal.Species;
            AnimalWeight.Text = selectedAnimal.Weight.ToString();
            if (selectedAnimal.Carnivorous)
            {
                CarnivorousTrue.IsChecked = true;
                CarnivorousFalse.IsChecked = false;
            }
            else
            {
                CarnivorousTrue.IsChecked = false;
                CarnivorousFalse.IsChecked = true;
            }
            AnimalCaptures.SelectedDate = selectedAnimal.CaptureDates.FirstOrDefault();
            AnimalOccurrences.Text = selectedAnimal.PlacesOfOccurrence.FirstOrDefault();

            currentAnimalId = selectedAnimal.Id;
        }

        private void OccurrencesButton_Click(object sender, RoutedEventArgs e)
        {
            occurences.Add(AnimalOccurrences.Text);
            AnimalOccurrences.Text = String.Empty;
        }

        private void CaptureDateButton_Click(object sender, RoutedEventArgs e)
        {
            captureDates.Add(AnimalCaptures.SelectedDate.GetValueOrDefault());
        }


        private void CreateApiHandler()
        {
            var baseAddress = "http://localhost:55064/animal/";
            _apiHandler = new ApiHandler(baseAddress);
        }

        private void GetData()
        {
            var animals = _apiHandler.Get<Animal>();

            if (animals == null || animals.Count <= 0)
            {
                return;
            }

            this.AnimalList.Items.Clear();

            foreach (var animal in animals)
            {
                this.AnimalList.Items.Add(animal);
            }
        }
    }
}
