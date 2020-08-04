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
        MediaPlayer mMediaPlayer;
        MediaSource ms;
        public static int count;

        public VideoPage1()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SwitchRoom();
        }

        private void SwitchRoom()
        {
            mMediaPlayer = CreateMediaPlayer();

            count++;
            if (count % 2 == 0)
                ms = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/1.mp4"));
            else
                ms = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/2.mp4"));

            mMediaPlayer.Source = ms;
            mMediaPlayer.Play();

            VideoObj.SetMediaPlayer(mMediaPlayer);
        }

        private MediaPlayer CreateMediaPlayer()
        {
            if (mMediaPlayer != null)
            {
                mMediaPlayer.Pause();
                mMediaPlayer.Dispose();
                mMediaPlayer = null;
                mMediaPlayer = new MediaPlayer();
            }
            else
            {
                mMediaPlayer = new MediaPlayer();
            }

            return mMediaPlayer;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ms.Dispose();
        }

        private void BackToHome_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }

        private void SwitchRoom_Click(object sender, RoutedEventArgs e)
        {
            SwitchRoom();
        }
    }
}
