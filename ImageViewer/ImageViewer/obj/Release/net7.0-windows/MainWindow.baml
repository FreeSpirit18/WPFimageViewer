   M S B A M L   `   `   `  ���� F  BImageViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ImageViewerImageViewer  W0clr-namespace:ImageViewer.MVVM.View.userControls"ImageViewer.MVVM.View.userControls  3clr-namespace:ImageViewer.MVVMImageViewer.MVVM      ImageViewer.MainWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:ImageViewer     IuserControls0clr-namespace:ImageViewer.MVVM.View.userControls     8emguConverterclr-namespace:ImageViewer.MVVM     $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=����#FFFAF3 � Title$ 
MainWindow��$	��450��$	��800�� � 	Resources     BooleanToVisibilityConverter��%   &	        �� .���   
Background#��[  � RowDefinitions �� $��3* ��� $	��80* ��� $	��17* �.+�0   *ImageViewer.MVVM.View.userControls.menuBar  $��0��$	��Top=�� $��1�� � ColumnDefinitions �� $	��40* ��� $	��40* ��� $	��20* �.+��� $��0��$�� 10 10 10 10q�$��Center=�$��Center=�����  �� 
TargetType" �� �� Triggers ��  �� Binding �� *ImageActive+	 �� Value	 true.a��� 
 �� Property
 � �� �� Value �� *DisplayImage.Path+ �� UpdateSourceTrigger$ PropertyChanged=���  �� *ImageActive+		 false.a��� 
 � ��  emty# [ �� $��1��$�� 10 10 10 10q�$��Center=�$��Center=����� " �� ��  �� *ImageActive+		 false.a��� 
 � y�$ Hidden=�^�  ^� 
Conditions ��  �� Binding �� *	Grayscale+ �� Value True��  �� *ImageActive+ true.0��� 
 � �� �� *	GrayImage+ �� 	Converter    ImageViewer.MVVM.Converter  ^�  ��  �� *	Grayscale+	 False��  �� *ImageActive+ true.0��� 
 � �� �� *
BgrImage+   � $��2��  SecondaryGridBackground#��[  �� $	��80* ��� $	��10* ��� $	��10* �.+��� $��0��.+�9� $��Stretch=���]� $	��100��.+��� $��15(�$
��Blur��$
��Left=�$��Center=�d�   
ToggleTest#��[ $��Right=�$��Center=� d� 	IsChecked �� *
BlurActive+ �� Mode$ TwoWay=�.���� $��
24,8,24,16q� �� Orientation$ Vertical=�  MaterialDesignBody#���  .+��� $��Blur value:����  � Minimum$ 1Q� � Maximum$ 89Q� �� IsSnapToTickEnabled$
 True�� �� TickFrequency$ 2Q� �� TickPlacement$
 Both=� � Value �� *	BlurValue+$ TwoWay=�{��� *
BlurActive+�� 9� $��Stretch=���]� $	��100��.+��� $��15(�$��Treshold��$
��Left=�$��Center=�d� $��0,8,0,0q�#��[ $��Right=�$��Center=� �� *TreshActive+$ TwoWay=�.���� $��
24,8,24,16q�$ Vertical=�#���  .+��� $��Treshold value:���� $ 0Q�$	 255Q� �� *
TreshValue+$ TwoWay=�{��� *TreshActive+�� $��Max treshold value:���� $ 0Q�$	 255Q� �� *MaxTreshValue+$ TwoWay=�{��� *TreshActive+��  �� DisplayMemberPath$	 Key������ *ThresholdTypes+ �� SelectedItem �� *SelectedThreshold+�� 9� $��Stretch=���]� $	��100��.+��� $��15(�$��Eroding��$
��Left=�$��Center=�d� $��0,8,0,0q�#��[ $��Right=�$��Center=� �� *ErodeDilateActive+$ TwoWay=�.���� $��
24,8,24,16q�$ Vertical=�.+��� $��Erode Iterations:���� $ 10Q�$ 0Q�$ 1Q�$
 Both=� �� *ErodeIterations+$ TwoWay=�{��� *ErodeDilateActive+�� $��Dilate Iterations:���� $ 10Q�$ 0Q�$ 1Q�$
 Both=� �� *DilateIterations+$ TwoWay=�{��� *ErodeDilateActive+�� 9� $��Stretch=�y��� *	Grayscale+# [  ��]� $	��100��.+��� $��15(�$
��Edge��$
��Left=�$��Center=�d� $��0,8,0,0q�#��[ $��Right=�$��Center=� �� *
EdgeActive+$ TwoWay=�.���� $��
24,8,24,16q�$ Vertical=�#���  .+��� $��Edge treshold 1:���� $ 0Q�$	 255Q� �� *
EdgeTresh1+$ TwoWay=�{��� *
EdgeActive+�� $��Edge treshold 2:���� $ 0Q�$	 255Q� �� *
EdgeTresh2+$ TwoWay=�{��� *
EdgeActive+�� �� $��1��$��Center=�$��Center=�$��	24,0,24,0q�$ 
Horizontal=�.+��� $��15(�$��
Grayscale:��d� #��[  �� *	Grayscale+$ TwoWay=��� $��2��$��Bottom=�$��
Save image��$��	15 5 15 5q����� *SaveCommand+� $��2��#��[ .+�7   1ImageViewer.MVVM.View.userControls.customListView  $��2��$��Center=�