using Xamarin.UITest;
using NUnit.Framework;
using System;

[TestFixture]
public class TestPlayback {
public IApp app;
[SetUp]
public void Setup() {
app = ConfigureApp.iOS.AppBundle("/var/folders/5j/zs9gmy094q9gj5cgv5hs3mqm0000gn/T/test-recorder/ios-injection/linked-12-04-15-9-18-47-app.app").StartApp();
}[Test]
public void NewTest ()
{
app.Tap(x => x.Class("Xamarin_Forms_Platform_iOS_FrameRenderer"));
app.Screenshot("Tapped on view Xamarin_Forms_Platform_iOS_FrameRenderer with Text: 'FORWARD'");
app.ScrollDown();
app.Tap(x => x.Text("FORWARD"));
app.Screenshot("Tapped on view Xamarin_Forms_Platform_iOS_LabelRenderer with Text: 'FORWARD'");
app.ScrollDown();
app.Tap(x => x.Text("BACKWARD"));
app.Screenshot("Tapped on view Xamarin_Forms_Platform_iOS_FrameRenderer with Text: 'BACKWARD'");
app.Tap(x => x.Text("BACKWARD"));
app.Screenshot("Tapped on view Xamarin_Forms_Platform_iOS_FrameRenderer with Text: 'BACKWARD'");
}}