using raist_desktop.BaseView.Base;
using raist_desktop.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace raist_desktop.BaseView
{
    class WindowViewModel : BaseViewModel
    {
        //Merupakan Class Base Dari seluruh window,mencangkup event, dan tampilan


        #region Private Member
        //tempat window disimpan
        private Window mWindow;
        //Margin invisible window paling luar
        private int mOuterMarginSize = 10;
        //Besar radius ujuna windows, 0 tidak ada radius
        private int mOuterRadius = 10;
        #endregion

        #region Public Properties
        //Minimum Height dan Width pada window
        public double WindowMinimumWidth { get; set; } = 400;
        public double WindowMinimumHeight { get; set; } = 400;


        public int ResizeBorder { get; set; } = 6;
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        //Content padding
        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        //Page saat ini
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Setup;


        //Jika fullscreen, hilangkan margin pada window
        public int OuterMarginSize
        {
            get { return mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize; }
            set { mOuterMarginSize = value; }
        }

        public Thickness OuterMarginSizeThickness
        {
            get { return new Thickness(OuterMarginSize); }
        }

        //Jika fullscreen, hilangkan rounded edge pada window
        public int WindowRadius
        {
            get { return mWindow.WindowState == WindowState.Maximized ? 0 : mOuterRadius; }
            set { mOuterRadius = value; }
        }

        public CornerRadius WindowCornerRadius
        {
            get { return new CornerRadius(WindowRadius); }
        }

        public int TitleHeight { get; set; } = 38;

        public GridLength TitleHeightGridLength
        {
            get { return new GridLength(TitleHeight); }
        }
        #endregion

        #region Constructor
        public WindowViewModel(Window window)
        {
            mWindow = window;
            //Event listener ketika window di resize
            mWindow.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            //Event pada window button
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            var resizer = new WindowResizer(mWindow);

            checkIfLogin();
        }
        #endregion


        #region App Event
        public void checkIfLogin()
        {
            if (Properties.Settings.Default.SESSION_IS_FIRST_TIME)
            {
                CurrentPage = ApplicationPage.Setup;
                Console.WriteLine("First Time Setup");
            }
            else
            {
                CurrentPage = ApplicationPage.Main;
                Console.WriteLine("Already Set");
            }
        }
        #endregion

        #region Command
        public ICommand MinimizeCommand { get; set; }

        public ICommand MaximizeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand MenuCommand { get; set; }

        #endregion

        #region Private Helper

        public Point GetMousePosition()
        {
            var position = Mouse.GetPosition(mWindow);
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        #endregion

    }
}
