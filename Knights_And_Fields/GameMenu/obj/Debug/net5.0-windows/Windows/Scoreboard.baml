   M S B A M L   `   `   `  ����C  ?GameMenu, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GameMenu.WindowsGameMenu.Windows  5   	   9!clr-namespace:GameMenu.ViewModelsGameMenu.ViewModels  5   	   !    GameMenu.Windows.Scoreboard   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:GameMenu.Windows     5   	   1VMS!clr-namespace:GameMenu.ViewModels     5   	     � 
ResizeMode$  NoResize=�5	   	    � Title$ 
Scoreboard��5
   	   $	��800��6   $
��1000��6)    � 	Resources 5         FocusVisual �� Property �� Value �� StrokeDashArray J� SnapsToDevicePixels  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 returnStyle �� 
TargetType	 �� 
TargetType
 ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	 �     &	 �     &	 �     &	     &	 ?    &	 b    &	 �    &	 �    &		 �    &	
 �    7   7  7  7  7  7  7  7  7  7 	 �� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$   $	 1 2R�6/   $
 true��6E   $��1��6`   #��� ��6t   5      5      5      �� 5   
   $ 	#FFDDDDDD��6;   �� 5   
   $ 	#FF707070��67   �� 5   
   $ 	#FFBEE6FD��6>   �� 5   
   $ 	#FF3C7FB1��6:   �� 5   
   $ 	#FFC4E5F6��6<   �� 5   
   $ 	#FF2C628B��68   �� 5   
   $ 	#FFF4F4F4��6=   �� 5   
   $ 	#FFADB2B5��69   �� 5   
   $ 	#FF838383��6=   �� 5   
   " ��6$   .�5      ��  �@�FocusVisualStyle8   61   �� 5        � ��8  6+   �� 5!       � ��8  6,   �� 5"       � ��# � ��6+   �� 5#       � ��$ 1q�60   �� 5$       � ��$ Center=�6;   �� 5%       � ��$ Center=�69   �� 5&       � ��$ 1q�6(   �� 5'       � �� 5(      �� 5)      "	 ��6&   .R�5*      �� $
 border��6!   #��z��61   #��z��6[   #��z��6�   $
 true��6�   ._�5+      �� $ contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   # z 6�   #��z��6  5,       5-      P� 5.       �@��IsDefaulted$
 true��6=   . �5/   "   �� $ border��6@   # � ��6T    � ��50      P� 51       �@J�IsMouseOver$
 true��6=   . �52   "   �� $ border��6?   8   � ��6S   �� 53   "   $ border��6@   8   � ��6T   54      P� 55       � ��$
 true��6;   . �56   "   �� $ border��6?   8   � ��6S   �� 57   "   $ border��6@   8   � ��6T   58      P� 59       � {�$ false��6;   . �5:   "   �� $ border��6?   8   � ��6S   �� 5;   "   $ border��6@   8   � ��6T   �� 5<   "   $ contentPresenter��6K   8 	  � ��6i   5=      5?      5A      5B      5C      ��5D      �� 5E   
    j� Stretch$
 Fill=�6    �� ImageSource$ /Images/Menu/gallery.png��6$    � DataContext 5G      -   'GameMenu.ViewModels.ScoreboardViewModel  5H   
   .��5K      � $��50 30 50 40q�6    � ColumnDefinitions 5L   
   �� 5M      $��5* �6   �� 5N      $��1* �6   .+�5Q   
   �� $��15(�6   $��20q�6    $��50q�6-   $��Stretch=�6Y   $��Stretch=�6~   $
��Bold"�6�   $��Arial)�6�   ��69   �� *	Players+��5R      �� 5S      .R�5T      �� 
���   �$��0q�61   ._�5U      �  5V      �� 5W   "   $��1* �63   �� 5X   "   $��1* �63   .+�5Z      �� $��25q�6=   $��0��6I   ��6$   �� *Name+6I   �� 5[      $��25q�6>   $��1��6J   ��6$   �� *Score+6J   5\      5]      5^      ��5a      �� 5b      $
 Fill=�6   $+ %/Images/Menu/galleryForTopButtons.png��6,   5d      �� 5f   
   #��� 
 6   $��30, 30, 30, 600q�67   $��1��6P   ��6`   �� *ExitCommand+��5g      �� 5h      $
 Fill=�6   $" /Images/Menu/return_idle.png��6,   5j      5l      5m      