   M S B A M L   `   `   `  ���� C  ?Velocity, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ExecutorUI
ExecutorUI  e aMicrosoft.Web.WebView2.Wpf, Version=1.0.2903.40, Culture=neutral, PublicKeyToken=2a8ab48044d2601ekLclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.WpfMicrosoft.Web.WebView2.Wpf     ExecutorUI.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:ExecutorUI     \WpfLclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.Wpf     -     � AllowsTransparency$
  True��
������  � WindowStyle$
 None=� � Title$ 
MainWindow��$	��363��$	��705�� � 
ResizeMode$ NoResize=�.����  �� CornerRadius$ 6��$��2q� �� Padding$ 6q�$
}�True�� J� SnapsToDevicePixels$
 True��$	��363��$	��Top=�$y�Visible=�._��� -    ��Name$ 
MainBorder�����	#FF191919$��4q�$ 6�����	#FF191919 J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  	   
BlurRadius$	 15Q�
   	Direction$	
 -90Q�   RenderingBias$ Quality=�   ShadowDepth$ 2Q�   Color$ black��._��  � ColumnDefinitions ��  � RowDefinitions �� $��* ��� $
��Auto �.+��� -    ��Name$ ExecuteButton��$��57��
�������$��22��$��	5,310,0,0q�$
��Left=�$	��Top=�
���  ��$��Execute������  �� 
TargetType" ��.R��� ���	#FF191919$ 6��
���   �$��0q�._��� $��Center=�$��Center=��� -   $ ClearButton��$��62��
�������$��22��$��
68,310,0,0q�$
��Left=�$	��Top=� J� RenderTransformOrigin$ 
1.53,0.455&�$��Clear����Z� ���� " ��.R��� ���	#FF191919$ 6��
���   �$��0q�._��� $��Center=�$��Center=��� -    ��Name$ expName��$��Velocity��$
��Left=�$��2,-1,0,0q�$	��Top=�
�������$	��187���� -   $ InjectButton��$��54��
�������$��22��$��621,310,0,0q�$
��Left=�$	��Top=�$ 
1.53,0.455&�$��Inject����Z� ���� " ��.R��� ���	#FF191919$ 6��
���   �$��0q�._��� $��Center=�$��Center=��� -    ��Name$ ScriptsListBox��$��543,27,8,34q����	#FF111111
���������Z� )  #Microsoft.Web.WebView2.Wpf.WebView2  -     Name$ ExecutorTextBox��$��10,28,143,36q�   Source$� �file:///C:/Users/yousm/Desktop/Folders/Visual%20Studio%20Projects/ExecutorUI/bin/Release/net8.0-windows/Bin/ace-editor/index.html?�$y�Hidden=��� -	   $ ClientsButton��$��54��
�������$��22��$��561,310,0,0q�$
��Left=�$	��Top=�$ 
1.53,0.455&�$��Clients����Z� ���� " ��.R��� ���	#FF191919$ 6��
���   �$��0q�._��� $��Center=�$��Center=��� $
��Left=�$��15��$��	656,5,0,0q�$	��Top=�$��15��$��/applered.png���� $
��Left=�$��15��$��	636,5,0,0q�$	��Top=�$��15��$��/appleorange.png��� -
    �Name$ ClientsFrame��$��10,25,143,34q����	#FF111111$y�Visible=�.+��� -   $��NoClientsFoundLabel��$��No Clients Found��$��Center=�$��Center=����	#FF383838$��35(���Z� �� $
��Auto=�$
��True��.���� -    ��Name$ clients_container�� �� Orientation$ Vertical=�$	��528��$	��280��$	��528��.+�� -   $ client_template�����	#FF1E1E1E$
��auto��$��54��$y�Hidden=�.+��� $��40��$��	7,7,481,7q�$��40��$��Resources\SonarIcon.ico���� -   $ client_hash_jobid��$��Client PID:��$
��Left=�$��49,0,0,0q�$	��Top=�$ -0.579,-0.155&�
�������$��11(�$	��200����Z� �� -   $ client_injection_status��$��Status: Not Attached��$
��Left=�$��	49,15,0,0q�$	��Top=�$ -0.579,-0.155&�
�������$��11(�$	��200����Z� �� -    ��Name$ select_client_checkbox��$��Inject��$
��Left=�$��	53,35,0,0q�$	��Top=�
�������$��96����Z� �� -   $# select_client_excute_checkbox��$��Execute��$
��Left=�$��
108,35,0,0q�$	��Top=�
�������$��96����Z� �� -   $ OpenScriptButton��$��62��
�������$��22��$��136,310,0,0q�$
��Left=�$	��Top=�$ 
1.53,0.455&�$
��Open����Z� ���� " ��.R��� ���	#FF191919$ 6��
���   �$��0q�._��� $��Center=�$��Center=��� -   $ SaveScriptButton��$��62��
�������$��22��$��204,310,0,0q�$
��Left=�$	��Top=�$ 
1.53,0.455&�$
��Save����Z� ���� " ��.R��� ���	#FF191919$ 6��
���   �$��0q�._��� $��Center=�$��Center=�