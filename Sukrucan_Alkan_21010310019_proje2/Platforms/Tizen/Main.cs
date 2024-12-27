using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Sukrucan_Alkan_21010310019_proje2;

class Program : MauiApplication
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	static void Main(string[] args)
	{
		var app = new Program();
		app.Run(args);
	}
}
