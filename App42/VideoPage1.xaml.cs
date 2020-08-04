using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App42
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideoPage1 : Page
    {
        MediaPlayer mMediaPlayer2;
        MediaSource ms;
        public static int count;
        DispatcherTimer myTimer;

        public VideoPage1()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;

            mMediaPlayer2 = new MediaPlayer();
            if (myTimer == null)
                myTimer = new DispatcherTimer();
            myTimer.Tick -= MyTimer_Tick;
            myTimer.Tick += MyTimer_Tick;
            myTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        private void MyTimer_Tick(object sender, object e)
        {
            count++;
            tb.Text = (count / 10.0).ToString() + " s before the video shown";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myTimer.Start();

            ms = MediaSource.CreateFromUri(new Uri("http://hlstct.douyucdn2.cn/dyliveflv3/7328539r4UHEpavU.m3u8?txSecret=9b44838c1eb8da592afbf79608ac5d78&txTime=5f290598&token=cpg-lenovoxj-0-7328539-6b6c6143c44dd73d5f5fbd1e0bd788be&did=&origin=ws&vhost=play3&tp=3562f952"));
            mMediaPlayer2.Source = ms;
            mMediaPlayer2.Play();
            mMediaPlayer2.MediaOpened -= MMediaPlayer2_MediaOpened;
            mMediaPlayer2.MediaOpened += MMediaPlayer2_MediaOpened;

            VideoObj.SetMediaPlayer(mMediaPlayer2);
        }

        private async void MMediaPlayer2_MediaOpened(MediaPlayer sender, object args)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                myTimer.Stop();
            });
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            mMediaPlayer2.Pause();
            ms.Dispose();
            count = 0;
            myTimer.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }
    }
}
