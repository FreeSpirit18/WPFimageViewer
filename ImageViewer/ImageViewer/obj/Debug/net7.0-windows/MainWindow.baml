   M S B A M L   `   `   `  ����F  BImageViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ImageViewerImageViewer  5   	   W0clr-namespace:ImageViewer.MVVM.View.userControls"ImageViewer.MVVM.View.userControls  5   	   3clr-namespace:ImageViewer.MVVMImageViewer.MVVM  5	   	       ImageViewer.MainWindow   5   
   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ImageViewer     5   	   IuserControls0clr-namespace:ImageViewer.MVVM.View.userControls     5   	   8emguConverterclr-namespace:ImageViewer.MVVM     5	   	   $��Regular"�5   
   $��13(�5   
   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   
      TextRenderingMode$
 Auto=�5   
   ���#FFFAF3 � Title$ 
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5          BooleanToVisibilityConverter��%   &	        �� 5   
   6'   5      .��5      �   
Background#��[ 6    � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $	��80* �6   �� 5      $	��17* �6   .+�5!   
   0   *ImageViewer.MVVM.View.userControls.menuBar  $��0��6   $	��Top=�6,   � 5#   
   $��1��6    � ColumnDefinitions 5%      �� 5&      $	��40* �6#   �� 5'      $	��40* �6#   �� 5(      $	��20* �6#   .+�5,      �� $��0��6   $�� 10 10 10 10q�6$   $��Center=�6:   $��Center=�6W   ��5-      �� 5.       �� 
TargetType" ��6    �� Triggers 5/      �� 50       �� Binding 6*   �� *ImageActive+	 �� Value	 true6J   .a�51   "   �� 
 �� Property
 � �� �� Value 6;   �� *DisplayImage.Path+ �� UpdateSourceTrigger$ PropertyChanged=�52      �� 53       6*   �� *ImageActive+		 false6J   .a�54   "   �� 
 � ��  emty# [ 6;   55      57      59      �� 5<      $��1��6   $�� 10 10 10 10q�6$   $��Center=�6:   $��Center=�6W   ��5=      �� 5>      " ��6    5?      �� 5@       6*   �� *ImageActive+		 false6J   .a�5A   "   �� 
 � y�$ Hidden=�6?   5B      ^� 5C       ^� 
Conditions 5D   "   �� 5E   &    �� Binding 60   �� *	Grayscale+ �� Value True6N   �� 5F   &    60   �� *ImageActive+ true6P   .0�5H   "   �� 
 � �� 6;   �� *	GrayImage+ �� 	Converter    ImageViewer.MVVM.Converter  5I      ^� 5K       5L   "   �� 5M   &    60   �� *	Grayscale+	 False6N   �� 5N   &    60   �� *ImageActive+ true6P   .0�5P   "   �� 
 � �� 6;   �� *
BgrImage+   5Q      5S      5U      � 5X      $��2��6     SecondaryGridBackground#��[ 6#    5Z      �� 5[      $	��80* �6$   �� 5\      $	��10* �6$   �� 5]      $	��10* �6$   .+�5`      �� $��0��6   .+�5a      9� $��Stretch=�6    ��5b      ]� 5c      $	��100��6(   .+�5d   "   �� $��15(�6(   $
��Blur��66   $
��Left=�6E   $��Center=�6`   d� 5e   "     
ToggleTest#��[ 6^   $��Right=�5f   +   $��Center=�6G    d� 	IsChecked 5e   /   �� *
BlurActive+ �� Mode$ TwoWay=�5f   G   5g      .��5i      �� $��
24,8,24,16q�5j       �� Orientation$ Vertical=�5k        MaterialDesignBody#���  5l      .+�5p      �� $��Blur value:��6$   �� 5q       � Minimum$ 1Q�6%    � Maximum$ 89Q�61    �� IsSnapToTickEnabled$
 True��5r   %    �� TickFrequency$ 2Q�6@    �� TickPlacement$
 Both=�6R    � Value 5q   >   �� *	BlurValue+$ TwoWay=�{�6g   �� *
BlurActive+5t      5v      5w      �� 5x      9� 5z      $��Stretch=�6    ��5{      ]� 5|      $	��100��6(   .+�5}   "   �� $��15(�6(   $��Treshold��66   $
��Left=�6I   $��Center=�6d   d� 5~   "   $��0,8,0,0q�6/   #��[ 6o   $��Right=�5   +   $��Center=�6G    5~   @   �� *TreshActive+$ TwoWay=�5   G   5�      .��5�      �� $��
24,8,24,16q�5�      $ Vertical=�5�      #���  5�      .+�5�      �� $��Treshold value:��6$   �� 5�      $ 0Q�6%   $	 255Q�61    6@   �� *
TreshValue+$ TwoWay=�{�6j   �� *TreshActive+�� 5�      $��Max treshold value:��6$   �� 5�      $ 0Q�6%   $	 255Q�61    6@   �� *MaxTreshValue+$ TwoWay=�{�6m   �� *TreshActive+�� 5�       �� DisplayMemberPath$	 Key��6y   ��6'   �� *ThresholdTypes+ �� SelectedItem 6N   �� *SelectedThreshold+6y   5�      5�      �� 5�      9� 5�      $��Stretch=�6    ��5�      ]� 5�      $	��100��6(   .+�5�   "   �� $��15(�6(   $��Eroding��66   $
��Left=�6H   $��Center=�6c   d� 5�   "   $��0,8,0,0q�6/   #��[ 6u   $��Right=�5�   +   $��Center=�6G    5�   @   �� *ErodeDilateActive+$ TwoWay=�5�   G   5�      .��5�      �� $��
24,8,24,16q�5�      $ Vertical=�5�      .+�5�      �� $��Erode Iterations:��6$   �� 5�      $ 10Q�6%   $ 0Q�62   $ 1Q�5�   !   $
 Both=�63    6H   �� *ErodeIterations+$ TwoWay=�{�6w   �� *ErodeDilateActive+�� 5�      $��Dilate Iterations:��6$   �� 5�      $ 10Q�6%   $ 0Q�62   $ 1Q�5�   !   $
 Both=�63    6H   �� *DilateIterations+$ TwoWay=�{�6x   �� *ErodeDilateActive+5�      5�      �� 5�      9� 5�      $��Stretch=�6    y�6>   �� *	Grayscale+# [  ��5�      ]� 5�      $	��100��6(   .+�5�   "   �� $��15(�6(   $
��Edge��66   $
��Left=�6E   $��Center=�6`   d� 5�   "   $��0,8,0,0q�6/   #��[ 6n   $��Right=�5�   +   $��Center=�6G    5�   @   �� *
EdgeActive+$ TwoWay=�5�   G   5�      .��5�      �� $��
24,8,24,16q�5�      $ Vertical=�5�      #���  5�      .+�5�      �� $��Edge treshold 1:��6$   �� 5�      $ 0Q�6%   $	 255Q�61    6@   �� *
EdgeTresh1+$ TwoWay=�{�6j   �� *
EdgeActive+�� 5�      $��Edge treshold 2:��6$   �� 5�      $ 0Q�6%   $	 255Q�61    6@   �� *
EdgeTresh2+$ TwoWay=�{�6j   �� *
EdgeActive+5�      5�      �� 5�      5�      �� 5�      $��1��6   $��Center=�6*   $��Center=�6H   $��	24,0,24,0q�5�      $ 
Horizontal=�60   .+�5�      �� $��15(�6   $��
Grayscale:��6*   d� 5�      #��[ 6Q    6$   �� *	Grayscale+$ TwoWay=�6Q   5�      �� 5�      $��2��6   $��Bottom=�6&   $��
Save image��6A   $��	15 5 15 5q�6V   ��6i   �� *SaveCommand+5�      5�      � 5�   
   $��2��6   #��[ 6   .+�5�      7   1ImageViewer.MVVM.View.userControls.customListView  $��2��6*   $��Center=�67   5�      5�      5�      