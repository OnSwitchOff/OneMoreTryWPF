﻿using OneMoreTryWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OneMoreTryWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow(MyInvoiceInfo myInvoiceInfo)
		{
			InitializeComponent();
			InvoiceViewModel viewModel = new InvoiceViewModel(myInvoiceInfo);
			DataContext = viewModel;
		}

		private void StackPanel_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{

		}

		//private void isEditable_Click(object sender, RoutedEventArgs e)
		//{
		//	if (isEditable.IsChecked == true)
		//	{
		//		led.Background = (Brush)TryFindResource("greenLed");
		//	}
		//	else
		//	{
		//		led.Background = (Brush)TryFindResource("redLed");
		//	}
		//}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt) // Is Alt key pressed
			{
				if (Keyboard.IsKeyDown(Key.G))
				{
					((InvoiceViewModel)DataContext).GodMode = !((InvoiceViewModel)DataContext).GodMode;
				}
			}
		}		
	}
}
