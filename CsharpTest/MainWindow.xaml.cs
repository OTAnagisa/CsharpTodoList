using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CsharpTest
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // Todoリスト キャッシュ
        private List<Todo> todos = new List<Todo>();

        // 初期表示
        public MainWindow()
        {
            InitializeComponent();
            RefreshTodoListBox();
            newTodoForm_Initialized();
            selectedTodoForm_Initialized();
        }

        // 追加ボタン押下時
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // Todo インスタンスを作成
            Todo todo = new Todo();

            todo.Contents = newTodoContents.Text;
            todo.CreatedDate = DateTime.Now;
            todo.LimitDate = newTodoLimit.SelectedDate ?? DateTime.Now;
            
            // Todoリストに追加
            todos.Add(todo);

            RefreshTodoListBox();
            newTodoForm_Initialized();
        }

        // 編集ボタン押下時
        private void editSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (todoListBox.SelectedItem == null)
            {
                return;
            }

            Todo todo = (Todo)todoListBox.SelectedItem;
            todo.Contents = selectedTodoContents.Text;
            todo.LimitDate = selectedTodoLimit.SelectedDate ?? DateTime.Now;

            RefreshTodoListBox();
            selectedTodoForm_Initialized();
        }

        // Todoリスト 初期化
        private void RefreshTodoListBox()
        {
            todoListBox.ItemsSource = null;
            todoListBox.ItemsSource = todos;
            todoListBox.DisplayMemberPath = "ContentsAndLimitDate";
        }

        // Todoリスト 選択時
        private void todoListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (todoListBox.SelectedItem == null)
            {
                return;
            }

            Todo todo = (Todo)todoListBox.SelectedItem;
            selectedTodoContents.Text = todo.Contents;
            selectedTodoCreatedDate.Text = todo.CreatedDate.ToString();
            selectedTodoLimit.SelectedDate = todo.LimitDate;
        }

        // 削除ボタン押下時
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (todoListBox.SelectedItem == null)
            {
                return;
            }

            todos.Remove((Todo)todoListBox.SelectedItem);

            RefreshTodoListBox();
            selectedTodoForm_Initialized();
        }

        // 追加フォーム 初期化
        private void newTodoForm_Initialized()
        {
            newTodoLimit.SelectedDate = null;
            newTodoContents.Text = "";
        }

        // 編集フォーム 初期化
        private void selectedTodoForm_Initialized()
        {
            selectedTodoLimit.SelectedDate = null;
            selectedTodoContents.Text = "";
            selectedTodoCreatedDate.Text = "";
        }
    }

    public class Todo
    { 
        public string Contents { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LimitDate { get; set; }

        public string ContentsAndLimitDate { 
            get { return $"{LimitDate.ToLongDateString()} - {Contents}"; }
        }
    }
}
