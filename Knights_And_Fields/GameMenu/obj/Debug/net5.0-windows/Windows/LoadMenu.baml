   M S B A M L   `   `   `  ����C  ?GameMenu, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GameMenu.WindowsGameMenu.Windows  5   	   9!clr-namespace:GameMenu.ViewModelsGameMenu.ViewModels  5   	       GameMenu.Windows.LoadMenu   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:GameMenu.Windows     5   	   1VMS!clr-namespace:GameMenu.ViewModels     5   	     � Title$  LoadMenu��5	   	   $	��800��6   $
��1000��6'    � 
ResizeMode$ NoResize=�5
   	   ��5      �� 5   
    j� Stretch$
 Fill=�6    �� ImageSource$  /Images/Menu/game_menu.png��6$    � DataContext 5      +  %GameMenu.ViewModels.LoadMenuViewModel  5   
    � 	Resources 5         	loadHover  deleteHover  returnHover  FocusVisual �� Property �� Value �� StrokeDashArray	 J� SnapsToDevicePixels  Button.Static.Background
 �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background 	 Button.Pressed.Border 
 Button.Disabled.Background  Button.Disabled.Border  Button.Disabled.Foreground  	loadStyle �� 
TargetType �� 
TargetType ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName  FocusVisual1  Button.Static.Background1  Button.Static.Border1   Button.MouseOver.Background1  Button.MouseOver.Border1  Button.Pressed.Background1  Button.Pressed.Border1  Button.Disabled.Background1  Button.Disabled.Border1  Button.Disabled.Foreground1  deleteStyle  FocusVisual2  Button.Static.Background2  Button.Static.Border2   Button.MouseOver.Background2  Button.MouseOver.Border2  Button.Pressed.Background2  Button.Pressed.Border2   Button.Disabled.Background2 ! Button.Disabled.Border2 " Button.Disabled.Foreground2 # returnStyle��%�  &	        &	 E     &	 �     &	 �     &	 �    &	 �    &	 �    &	 �    &	     &		 5    &	
 X    &	 {    &	 �    &	 �    7  7  7  7   7  7   7 	 7 
 7  7  &	 �    &	 v    &	 �    &	 �    &	 �    &	 	    &	 %	    &	 H	    &	 k	    &	 �	    &	 �	    7  7  7  7  7  7  7  7  7  7  &	 �    &	 f    &	 �    &	 �    &	 �    &	 �    &	     &	  8    &	! [    &	" ~    &	# �    7  7  7  7  7  7  7  7   7 ! 7 " �� 5   
   $
 Fill=�6'   $! /Images/Menu/load_hover.png��66   �� 5   
   $
 Fill=�6)   $# /Images/Menu/delete_hover.png��68   �� 5   
   $
 Fill=�6)   $# /Images/Menu/return_hover.png��68   �� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$   $	 1 2R�6/   #��� ��6E   $
	 true��6�   $��1��6�   5      5       5!      �� 5"   
   $
 	#FFDDDDDD��6;   �� 5#   
   $
 	#FF707070��67   �� 5$   
   $
 	#FFBEE6FD��6>   �� 5%   
   $
 	#FF3C7FB1��6:   �� 5&   
   $
 	#FFC4E5F6��6<   �� 5'   
   $
 	#FF2C628B��68   �� 5(   
   $
 	#FFF4F4F4��6=   �� 5)   
   $
 	#FFADB2B5��69   �� 5*   
   $
 	#FF838383��6=   �� 5+   
   " ��6"   .�5,      ��  �@�FocusVisualStyle8   61   �� 5-       � ��8  6+   �� 5.       � ��8  6,   �� 5/       � ��# � ��6+   �� 50       � ��$ 1q�60   �� 51       � ��$ Center=�6;   �� 52       � ��$ Center=�69   �� 53       � ��$ 1q�6(   �� 54       � �� 55      �� 56      " ��6&   .R�57      �� $ border��6!   #��z��61   #��z��6[   #��z��6�   $
	 true��6�   ._�58      �� $ contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   #	 z	 6�   #��z��6  59       5:      P� 5;       �@��IsDefaulted$
 true��6=   . �5<   "   �� $ border��6@   # � ��6T    � ��5=      P� 5>       �@J�IsMouseOver$
 true��6=   . �5?   "   �� $ border��6?   8   � ��6S   �� 5@   "   $ border��6@   8   � ��6T   5A      P� 5B       � ��$
 true��6;   . �5C   "   �� $ border��6?   8   � ��6S   �� 5D   "   $ border��6@   8   � ��6T   5E      P� 5F       � {�$ false��6;   . �5G   "   �� $ border��6?   8   � ��6S   �� 5H   "   $ border��6@   8   � ��6T   �� 5I   "   $ contentPresenter��6K   8 	  � ��6i   5J      5L      5N      5O      �� 5P   
   .�5Q      ��  � �� 5R      �� 5S      .R�5T      �� $��2q�6$   $	 1 2R�6/   #��� ��6E   $
	 true��6�   $��1��6�   5U      5W      5X      �� 5Y   
   $
 	#FFDDDDDD��6<   �� 5Z   
   $
 	#FF707070��68   �� 5[   
   $
 	#FFBEE6FD��6?   �� 5\   
   $
 	#FF3C7FB1��6;   �� 5]   
   $
 	#FFC4E5F6��6=   �� 5^   
   $
 	#FF2C628B��69   �� 5_   
   $
 	#FFF4F4F4��6>   �� 5`   
   $
 	#FFADB2B5��6:   �� 5a   
   $
 	#FF838383��6>   �� 5b   
   " ��6$   .�5c      ��  �@�FocusVisualStyle8   61   �� 5d       � ��8  6+   �� 5e       � ��8  6,   �� 5f       � ��# � ��6+   �� 5g       � ��$ 1q�60   �� 5h       � ��$ Center=�6;   �� 5i       � ��$ Center=�69   �� 5j       � ��$ 1q�6(   �� 5k       � �� 5l      �� 5m      " ��6&   .R�5n      �� $ border��6!   #��z��61   #��z��6[   #��z��6�   $
	 true��6�   ._�5o      �� $ contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   #	 z	 6�   #��z��6  5p       5q      P� 5r       �@��IsDefaulted$
 true��6=   . �5s   "   �� $ border��6@   # � ��6T    � ��5t      P� 5u       �@J�IsMouseOver$
 true��6=   . �5v   "   �� $ border��6?   8   � ��6S   �� 5w   "   $ border��6@   8   � ��6T   5x      P� 5y       � ��$
 true��6;   . �5z   "   �� $ border��6?   8   � ��6S   �� 5{   "   $ border��6@   8   � ��6T   5|      P� 5}       � {�$ false��6;   . �5~   "   �� $ border��6?   8   � ��6S   �� 5   "   $ border��6@   8   � ��6T   �� 5�   "   $ contentPresenter��6K   8 	  � ��6i   5�      5�      5�      5�      �� 5�   
   .�5�      ��  � �� 5�      �� 5�      .R�5�      �� $��2q�6$   $	 1 2R�6/   #��� ��6E   $
	 true��6�   $��1��6�   5�      5�      5�      �� 5�   
   $
 	#FFDDDDDD��6<   �� 5�   
   $
 	#FF707070��68   �� 5�   
   $
 	#FFBEE6FD��6?   �� 5�   
   $
 	#FF3C7FB1��6;   �� 5�   
   $
 	#FFC4E5F6��6=   �� 5�   
   $
 	#FF2C628B��69   �� 5�   
   $
 	#FFF4F4F4��6>   �� 5�   
   $
 	#FFADB2B5��6:   �� 5�   
   $
 	#FF838383��6>   �� 5�   
   " ��6$   .�5�      ��  �@�FocusVisualStyle8   61   �� 5�       � ��8  6+   �� 5�       � ��8  6,   �� 5�       � ��# � ��6+   �� 5�       � ��$ 1q�60   �� 5�       � ��$ Center=�6;   �� 5�       � ��$ Center=�69   �� 5�       � ��$ 1q�6(   �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      �� $ border��6!   #��z��61   #��z��6[   #��z��6�   $
	 true��6�   ._�5�      �� $ contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   #	 z	 6�   #��z��6  5�       5�      P� 5�       �@��IsDefaulted$
 true��6=   . �5�   "   �� $ border��6@   # � ��6T    � ��5�      P� 5�       �@J�IsMouseOver$
 true��6=   . �5�   "   �� $ border��6?   8   � ��6S   �� 5�   "   $ border��6@   8   � ��6T   5�      P� 5�       � ��$
 true��6;   . �5�   "   �� $ border��6?   8   � ��6S   �� 5�   "   $ border��6@   8   � ��6T   5�      P� 5�       � {�$ false��6;   . �5�   "   �� $ border��6?   8   � ��6S   �� 5�   "   $ border��6@   8   � ��6T   �� 5�   "   $ contentPresenter��6K   8 	  � ��6i   5�      5�      5�      5�      5�      .��5�      � $��50 30 50 40q�6    � ColumnDefinitions 5�   
   �� 5�      $��1* �6   �� 5�      $��1* �6   .+�5�   
   �� $��144,53,10,74q�6   $��Visible=�6-   $��Visible=�6S   .��5�      � .+�5�      �� $��15(�6   $��Stretch=�6q   $��Stretch=�6�   $
��Bold"�6�   $��Arial)�6�   ��6(   �� *
SavedFiles+ �� SelectedItem 6K   �� *SelectedFile+��5�      �� 5�      .R�5�      �� 
���   �$��2q�69   ._�5�   "   � .+�5�   &   �� $��25q�6L   ��6,   �� *DisplayName+6L   5�   #   5�      5�      ��5�      �� 5�      $
 Fill=�6%   $+ %/Images/Menu/galleryForTopButtons.png��64   5�      5�      5�      � 5�   
   $��1��6   $��50, 75, 150, 75q�6    � RowDefinitions 5�      �� 5�      $��1* �6    6.   �� 5�      $��1* �6    6.   �� 5�      $��1* �6    6.   .+�5�      �� #���  6   $��0��69   $��30, 70q�6F   ��6V   �� *LoadCommand+��5�      �� 5�      $
 Fill=�6!   $  /Images/Menu/load_idle.png��60   5�      �� 5�      #���  6   $��1��6;   $��30, 70q�6H   ��6X   �� *DeleteCommand+��5�      �� 5�      $
 Fill=�6!   $" /Images/Menu/delete_idle.png��60   5�      5�      �� 5�   
   #��� # 6   $��238,81,150,574q�67   $��1��6O   ��6_   �� *ExitCommand+��5�      �� 5�      $
 Fill=�6   $" /Images/Menu/return_idle.png��6,   5�      5�      5�      