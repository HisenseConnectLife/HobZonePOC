using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using HobZonePOC.Models;
using HobZonePOC.Views;
using HobZonePOC.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Essentials;

namespace HobZonePOC.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class DrawPage : ContentPage
    {
        private HobViewModel viewModel;
        private ZoneInfo zoneInfo;

        public DrawPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new HobViewModel();
            zoneInfo = viewModel.GetZoneInfo();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

           
        }

        SKPaint circleBorder = new SKPaint
        {
            IsAntialias = true,
            Style = SKPaintStyle.Stroke,
            Color = SKColors.White,
            StrokeWidth = 5,
            PathEffect = SKPathEffect.CreateDash(new float[] { 20, 5 }, 0)
        };

        SKPaint circleBorderFull = new SKPaint
        {
            IsAntialias = true,
            Style = SKPaintStyle.Stroke,
            Color = SKColors.White,
            StrokeWidth = 5,
        };

        SKPaint outlinePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 5,
            Color = SKColors.Gray,
        };


        SKPaint textPaint = new SKPaint
        {
            Color = SKColors.Red
        };

        Random rnd = new Random();


        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            double width = mainDisplayInfo.Width;
            double hobWidth = double.Parse(zoneInfo.Width.Replace(" cm", String.Empty));
            var unit = Convert.ToInt32(width / hobWidth);
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.Black);
            //DrawZone(canvas, unit, zoneInfo.Zone);
            DrawZone(canvas, unit, zoneInfo.Zone2);
            DrawZone(canvas, unit, zoneInfo.Zone3);
            DrawZone(canvas, unit, zoneInfo.Zone4);
            DrawZone(canvas, unit, zoneInfo.Zone5);
            DrawZone(canvas, unit, zoneInfo.Zone6);

            DrawBridge(canvas, unit, zoneInfo.Zone, zoneInfo.Zone3);
        }

        private void DrawBridge(SKCanvas canvas, int unit, Zone zoneUp, Zone zoneDown)
        {
            SKRect rect = new SKRect(
                (float)(zoneUp.X * unit - zoneUp.W * unit /2) ,
                (float)(zoneUp.Y * unit - zoneUp.W * unit / 2),
                    (float)(zoneUp.X * unit + zoneUp.W * unit / 2),
                        (float)(zoneUp.Y * unit + zoneUp.W * unit / 2));
            float startAngle = 180;
            float sweepAngle = 180;

            

            //canvas.DrawOval(rect, circleBorder);

            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, startAngle, sweepAngle);
                canvas.DrawPath(path, outlinePaint);
            }
            canvas.DrawLine((float)(zoneUp.X * unit - zoneUp.W * unit / 2),
                (float)(zoneUp.Y * unit ),
                (float)(zoneDown.X * unit - zoneDown.W * unit / 2),
                (float)(zoneDown.Y * unit - 35), outlinePaint);

            canvas.DrawLine((float)(zoneUp.X * unit + zoneUp.W * unit / 2),
                (float)(zoneUp.Y * unit),
                (float)(zoneDown.X * unit + zoneDown.W * unit / 2),
                (float)(zoneDown.Y * unit - 35), outlinePaint);

            string str = $"{rnd.Next(1, 100)} °C";
            var textWidth = textPaint.MeasureText(str);
            textPaint.TextSize = 0.9f * (zoneUp.W.Value * unit / 2) * textPaint.TextSize / textWidth;
            canvas.DrawText(str, (zoneUp.X.Value * unit) - textPaint.MeasureText(str) / 2, zoneUp.Y.Value * unit + textPaint.TextSize / 2, textPaint);

        }

        private void DrawZone(SKCanvas canvas, int unit, Zone zone)
        {
            if (zone.Present)
            {
                bool isActive = rnd.Next(2) == 0;
                if (isActive)
                {
                    string str = $"{rnd.Next(1,100)} °C";
                    canvas.DrawCircle(zone.X.Value * unit, zone.Y.Value * unit, zone.W.Value * unit / 2, circleBorderFull);
                    var textWidth = textPaint.MeasureText(str);
                    textPaint.TextSize = 0.9f * (zone.W.Value * unit / 2) * textPaint.TextSize / textWidth;
                    canvas.DrawText(str, (zone.X.Value * unit) - textPaint.MeasureText(str) / 2, zone.Y.Value * unit + textPaint.TextSize / 2, textPaint);
                }
                else
                {
                    canvas.DrawCircle(zone.X.Value * unit, zone.Y.Value * unit, zone.W.Value * unit / 2, circleBorder);
                }
            }
        }
    }
}