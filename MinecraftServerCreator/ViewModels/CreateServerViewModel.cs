﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Wpf.Ui.Common.Interfaces;
using ApiConsumer;
using System;
using Domain.Enum;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics.Metrics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Domain;
using Microsoft.Win32;
using System.Windows.Forms;

namespace MinecraftServerCreator.ViewModels
{
    public partial class CreateServerViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private bool _isCreated = true;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MinecraftVersionGroups))]
        private string? _selectedServerType = new string(string.Empty);

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MinecraftVersions))]
        private string? _selectedServerVersionGroup = new string(string.Empty);
        
        [ObservableProperty]
        private string? _selectedServerVersion = new string(string.Empty);

        [ObservableProperty]
        private string _instanceDirectory = new string(string.Empty);

        [ObservableProperty]
        private IList<string> _serverTypes = new ObservableCollection<string>();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SelectedServerVersion))]
        private IList<string> _minecraftVersions = new ObservableCollection<string>();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SelectedServerVersionGroup))]
        private IList<string> _minecraftVersionGroups = new ObservableCollection<string>();

        private IList<string> _minecraftVersionGroupsReadable = new ObservableCollection<string>();
        private IList<string> _minecraftVersionsReadable = new ObservableCollection<string>();

        private readonly IServiceProvider _serviceProvider;
        private dynamic _apiConsumer;


        public CreateServerViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OnNavigatedFrom()
        {
        }

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            _serverTypes = new ObservableCollection<string>(Enum.GetNames(typeof(ServerType)).ToList());

            _isInitialized = true;
        }

        partial void OnSelectedServerTypeChanging(string? value)
        {
            dynamic response = null;
            
            switch (value)
            {
                case "Paper":
                    _apiConsumer = _serviceProvider.GetService<IApiOperator<Domain.Paper.ProjectVersions>>();
                    response = _apiConsumer.GetMinecraftVersionsAsync().Result;
                    break;
                case "Purpur":
                    _apiConsumer = _serviceProvider.GetService<IApiOperator<Domain.Purpur.ProjectVersions>>();
                    response = _apiConsumer.GetMinecraftVersionsAsync().Result;
                    break;
                case "Pufferfish":
                    _apiConsumer = _serviceProvider.GetService<IApiOperator<Domain.Pufferfish.ProjectVersions>>();
                    response = _apiConsumer.GetMinecraftVersionsAsync().Result;
                    break;
            }

            _selectedServerType = value;
            _minecraftVersionGroupsReadable = new ObservableCollection<string>(response.VersionGroups);
            _minecraftVersionsReadable = new ObservableCollection<string>(response.Versions);
            
        }

        partial void OnSelectedServerTypeChanged(string? value)
        {
            _selectedServerVersionGroup = string.Empty;
            _selectedServerVersion = string.Empty;
            _minecraftVersionGroups = new ObservableCollection<string>(_minecraftVersionGroupsReadable);
        }

        partial void OnSelectedServerVersionGroupChanging(string? value)
        {
            if (value is not null)
            {
                _selectedServerVersionGroup = value;
                _minecraftVersions = _minecraftVersionsReadable.Where(x => x.Contains(_selectedServerVersionGroup)).ToList();
            }
        }

        [RelayCommand]
        private async Task OnServerCreation()
        {
            IsCreated = false;

            if (Directory.Exists(_instanceDirectory))
            {
                await _apiConsumer.DownloadMinecraftServerInstance(_selectedServerType, _selectedServerVersionGroup, _selectedServerVersion, _instanceDirectory);
            }

            IsCreated = true;
        }

        [RelayCommand]
        private async Task ChooseDirectory()
        {
            var dialog = new FolderBrowserDialog();
            dialog.SelectedPath = @"C:\";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                InstanceDirectory = dialog.SelectedPath + "\\";
            }
        }
    }
}
