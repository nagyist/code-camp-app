﻿using CodeCamp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CodeCamp.Models;

namespace CodeCamp
{
	public class SessionsPage : ContentPage
	{
		ListView listView;
    SessionsViewModel viewModel;
		public SessionsPage ()
		{
			Title = "Sessions";
      this.BindingContext = viewModel = new SessionsViewModel();
			NavigationPage.SetHasNavigationBar (this, true);

      var refresh = new ToolbarItem
      {
        Command = viewModel.LoadSessionsCommand,
        Icon = "refresh.png",
        Name = "refresh",
        Priority = 0
      };

      ToolbarItems.Add(refresh);


			listView = new ListView {
				RowHeight = 40
			};
			// see the SessionCell implementation for how the variable row height is calculated
			listView.HasUnevenRows = true;

			listView.ItemsSource = viewModel.SessionsGrouped;
			listView.IsGroupingEnabled = true;
			listView.GroupDisplayBinding = new Binding("Key"); //this is our key property on grouping. - See more at: http://motzcod.es/#sthash.QlKNtfa4.dpuf

      var cell = new DataTemplate(typeof(TextCell));
      cell.SetBinding(TextCell.TextProperty, "Name");
      cell.SetBinding(TextCell.DetailProperty, "MainPresenter.DisplayName");

      listView.ItemTemplate = cell;


			listView.ItemSelected += (sender, e) => {
				if(listView.SelectedItem == null)
					return;
				var session = e.SelectedItem as Session;
				var sessionPage = new SessionPage(session);
				Navigation.PushAsync(sessionPage);
				listView.SelectedItem = null;
			};

      var activityIndicator = new ActivityIndicator();
      activityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
      activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {activityIndicator, listView}
			};
		}

    protected override void OnAppearing()
    {
      base.OnAppearing();
      if (viewModel.IsInitialized)
        return;

      viewModel.IsInitialized = true;
      viewModel.LoadSessionsCommand.Execute(null);
    }
	}
}