   M S B A M L   `   `   `  ����C  ?GameMenu, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GameMenu.WindowsGameMenu.Windows  5   	   *    $GameMenu.Windows.CreateNewGameWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:GameMenu.Windows     5   	   $	��200��5   	   $	��450��6     � 
ResizeMode$  NoResize=�5	   	   ��5
      �� 5   
    j� Stretch$
 Fill=�6    �� ImageSource$ /Images/Menu/gallery.png��6$   .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��3* �6   .+�5   
   �� $,��&Please write your name young warrior: ��6   $��0��6A   $��4��6Q   $��2��6e   $��0��6v   $��16(�6�   $��
100,40,0,0q�6�   $��Italic$�5      $	��200��6#   $	��450��63   �� 5   
   -   5         ��Name$ 
playerName��5      $��1��6&   $��2��66   $��2��6J   �� 5    
   -   5         ��Name$ startButton��5       $��1��6&   $��2��66   $��3��6J   $��10, 10, 10, 20q�6W   ��5!      Z� ��6'   Z� ��5"      �� 5#      $! /Images/Menu/start_idle.png��6   $
 Fill=�6G   5%      5'      5(      