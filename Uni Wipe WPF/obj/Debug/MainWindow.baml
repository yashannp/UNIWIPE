   M S B A M L   `   `   `  ����G  CUni Wipe WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Uni_Wipe_WPFUni_Wipe_WPF  5   	       Uni_Wipe_WPF.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Uni_Wipe_WPF     5   	     � Title$  
MainWindow��5   	   $��534.545��6   $��708.485��6-    � 
ResizeMode$ NoResize=�6=    � WindowStartupLocation$ CenterScreen=�6S    � AllowsTransparency .  � WindowStyle$
 None=�6�   ��6x   Z� .��5	      � ��5
   
   �� 5       �� ImageSource$5 /pack://siteoforigin:,,,/Image/HDD wipe tool.png��6    � ColumnDefinitions 5   
   �� 5      .+�5   
   �� -   5         ��Name$
 Mbt1��5      $
��Exit��6   $
��Left=�6.   $��32,487,0,28q�6I   $	��104��6^   $��20��6   ��5      �� 5       �� EndPoint$ 0.5,1&�6&   	 �� 
StartPoint$	 0.5,0&�67   .��5      � 
 � Color$
 	#FFEC102E��6#    � Offset$
 0.37Q�65   � 5      $
 White��6#   $ 1Q�61   � 5      $
 	#FF828282��6#   $ 0.635Q�65   5      ��5      �� 5       �� 
TargetType" ��6   .�5       ��  �� Property � �� �� Value 5!      �� 5"       �� 
TargetType" ��6.   .R�5#   "   ��  ��Name$ button��6d    �� CornerRadius$ 5��6)   #��z��6:   $	��100��6t   ._�5$   &   � .+�5%   *   �� $��1,0,1,1q�61   
���   � 6_   �� *CornerRadius+ �� ElementName$ button�� J� Effect 5&   .   -  'System.Windows.Media.Effects.BlurEffect  5'   2      Radius$ 2Q�6=      
KernelType$ Gaussian=�6H   5)   +   �� 5*   *   $��0,1,0,0q�61   
�������$��2q�6_    J� Opacity$	 0.7Q�6j    6x   �� *CornerRadius+$ button�� 5+   .     5,   2   $ 2Q�6=   $ Gaussian=�6H   5.   +   �� 5/   *   #��z��6;   #��z��6k   #��z��6�   $��Center=�6�   $��Center=�6�   #��z��6  50   '   51   #   52      54      �� 55       � �� �#00206063   �� 56       � ��
 �����63   �� 57       � ��$ 20(�61   �� 58       � ��$ Consolas)�63   59      5;      �� 5?   
   -   5        $
 MBt2��5?      $��Enter��6   $��Center=�6/   $��585,489,23,26q�6L   $	��100��6c    J� RenderTransformOrigin$ 0.501,-0.258&�6o   $��20��6�   ��5@      �� 5A      $ 0.5,1&�6&   $	 0.5,0&�67   .��5B      � $
 	#FF2C0EE8��6#   $	 0.6Q�65   � 5C      $
 White��6#   $ 1Q�61   � 5D      $
 	#FF828282��6#   $ 0.491Q�65   5E      ��5G      �� 5H      " ��6   .�5I      ��  � �� 5J      �� 5K      " ��6.   .R�5L   "   �� $ button��6d   $ 5��6)   #��z��6:   $	��100��6t   ._�5M   &   � .+�5N   *   �� $��1,0,1,1q�61   
���   � 6_   �� *CornerRadius+$ button�� 5O   .     5P   2   $ 2Q�6=   $ Gaussian=�6H   5R   +   �� 5S   *   $��0,1,0,0q�61   
�������$��2q�6_   $	 0.7Q�6j    6x   �� *CornerRadius+$ button�� 5T   .     5U   2   $ 2Q�6=   $ Gaussian=�6H   5W   +   �� 5X   *   #��z��6;   #��z��6k   #��z��6�   $��Center=�6�   $��Center=�6�   #��z��6  5Y   '   5Z   #   5[      5]      �� 5^       � �� �#00206063   �� 5_       � ��
 �����63   �� 5`       � ��$ 20(�61   �� 5a       � ��$ Consolas)�63   5b      5d      �� 5k   
   -   5         ��Name$ label_1��5k      $����Warning:
Data is precious; handle it carefully. If the disk is 
physically damaged, it cannot be recovered. 
Before formatting, think twice. ��6!   $
��Left=�6�   $��379,277,0,0q�6�   $	��Top=�6  $	��328��6  $��84��6(  $��1q�64  $	 0.7Q�6H  $��13.333(�6V  $
��Bold"�6h  
���  ��
���������5l      �� 5m      $ 0.5,1&�6&   $	 0.5,0&�67   .��5n      � $
 Black��6#   $ 0Q�61   � 5o      $
 	#FFF19817��6#   $ 1Q�65   5p      5r      �� 5s   
   -   5        $ label_2��5s      $�� ��6!   $
��Left=�6,   $��	-19,2,0,0q�6G   $	��Top=�6Z   $	��469��6r   $	��114��6~   $��0q�6�   ��5t      �� 5u      $8 2pack://siteoforigin:,,,/Image/Uni Wipe Hedding.png��6   ��5w      �� 5x      $ 0.5,1&�6&   $	 0.5,0&�67   .��5y      � $
 Black��6#   $ 0Q�61   � 5z      $
 	#FF1037E6��6#   $ 0.657Q�65   5{      5}      �� 5~   
   -   5        $ label_3��5~      $�� ��6!   $
��Left=�6,   $	��122��6G   $��	2,360,0,0q�6T   $	��Top=�6g   $	��590��6   $��0q�6�   
����������	#FF0C0202$	 0.2Q�6�   �� 5   
   -   5        $ label��5      $��System��6   $
��Left=�60   $��
13,368,0,0q�6K   $	��Top=�6_   $	��125��6w   
�������$��16(�6�   $
��Bold"�6�   �� 5�   
   -   5        $ label1��5�      $��Label��6    $
��Left=�60   $��153,368,0,0q�6K   $	��Top=�6`   $	��389��6x   
�������$��13.333(�6�   $
��Bold"�6�   �� 5�   
   -   5        $ label2��5�      $��	Processer��6    $
��Left=�64   $��
13,402,0,0q�6O   $	��Top=�6c   $	��103��6{   
�������$��16(�6�   $
��Bold"�6�   �� 5�   
   -	   5        $ label3��5�      $��Label��6    $
��Left=�60   $��153,401,0,0q�6K   $	��Top=�6`   $	��388��6x   
�������$��13.333(�6�   $
��Bold"�6�   �� 5�   
   -
   5        $ label4��5�      $	��RAM��6    $
��Left=�6.   $��
13,435,0,0q�6I   $	��Top=�6]   $��81��6u   
�������$��16(�6�   $
��Bold"�6�   �� 5�   
   -   5        $ label5��5�      $��Label��6    $
��Left=�60   $��153,433,0,0q�6K   $	��Top=�6`   $	��395��6x   
�������$��13.333(�6�   $
��Bold"�6�   �� 5�   
   -   5        $ BtInfo��5�      $�� ��6!   $
��Left=�6,   $��646,394,0,0q�6G   $	��Top=�6\   $��27��6t   $��22��6   ���	#FFF73206��5�      �� 5�      $, &pack://siteoforigin:,,,/Image/info.png��6    j� Stretch$ Uniform=�6R   5�      5�      5�      