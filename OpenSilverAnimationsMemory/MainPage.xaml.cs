using OpenSilver;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace OpenSilverAnimationsMemory
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Enter construction logic here...
            Interop.ExecuteJavaScriptVoid("console.clear()");
        }

        private void AnimateButton_Click(object sender, RoutedEventArgs e)
        {
            // Opacity animation
            DoubleAnimationUsingKeyFrames opacityAnimation = new DoubleAnimationUsingKeyFrames();
            opacityAnimation.Duration = TimeSpan.FromSeconds(2);

            // KeyFrame 1: Initial opacity (0%)
            LinearDoubleKeyFrame opacityKF1 = new LinearDoubleKeyFrame();
            opacityKF1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            opacityKF1.Value = 0;
            opacityAnimation.KeyFrames.Add(opacityKF1);

            // KeyFrame 2: Half opacity (50%) at 1 second
            LinearDoubleKeyFrame opacityKF2 = new LinearDoubleKeyFrame();
            opacityKF2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1));
            opacityKF2.Value = 0.5;
            opacityAnimation.KeyFrames.Add(opacityKF2);

            // KeyFrame 3: Full opacity (100%) at 2 seconds
            LinearDoubleKeyFrame opacityKF3 = new LinearDoubleKeyFrame();
            opacityKF3.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2));
            opacityKF3.Value = 1;
            opacityAnimation.KeyFrames.Add(opacityKF3);

            // Create a Storyboard to apply the animation
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(opacityAnimation);

            // Set the target properties for the opacity animation
            Storyboard.SetTarget(opacityAnimation, MyRectangle);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("(UIElement.Opacity)"));

            // Set the storyboard to repeat forever
            storyboard.RepeatBehavior = RepeatBehavior.Forever;

            // Start the storyboard
            storyboard.Begin();
        }

        private DependencyObject MyRectangle => SP.Children.Last();

        private void Delete(object sender, RoutedEventArgs e)
        {
            SP.Children.Remove(SP.Children.Last());
        }

        private void GCollect(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
    
    public class TestRectangle: Rectangle
    {
        ~TestRectangle()
        {
            Console.WriteLine("Collected");
        }
    }
}
