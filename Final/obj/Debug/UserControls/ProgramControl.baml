   M S B A M L   `   `   `  ����B  >Power 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:FinalFinal  5          Final.ProgramControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      localclr-namespace:Final  5        � 	Resources  5	         Load g� Duration �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� Value   EasingFunction.  (System.Windows.Media.Animation.PowerEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode��%�   &	        �� 5
   
   .��5      U� $ 	0:0:0.420D�6,   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�6A   $ ItemToolTip��6�   .\�5        $ 0Q�6(    5        5      $ 	EaseInOut=�6$   5      5      5      5       � Triggers 5      :� 5   
    :� RoutedEvent$ Grid.Loaded��6   	 :� 
SourceName$	 ItemToolTip��61   .[�5      �� -   5        
 ��Name$
 Move0_BeginStoryboard��5      #��[  6=   5      :� 5   
   $ Grid.Loaded��6   $	 ItemToolTip��61   .[�5      �� .��5      �� .��5      W� $ Opacity�6&   $ ItemToolTip��6J   	 W� To$ 1X�6n   $ 0:0:0.4D�6u   5      5      5      .��5!      � -   5         �Name$ ItemToolTip��5!       J� Opacity$ 0Q�6    .+�5"   
   �  � ToolTip 6   �� *ProgramToolTip+.+�5#      ��   ButtonAccentRevealStyle#��[ 6   {�.  $	��584��6Y   $��50��6e   $��7q�6q   ��6}   Z� .��5$      � $��48��6   $	��586��6#   .+�5%      �� -   5        $��	ItemImage��5%      $��	5,5,541,7q�6N    �� BitmapScalingMode$
 Fant=�6a   $��Uniform=�6�   ��6-   �� *ProgramImage+6�   �� 5&      -   5        $��ItemName��5&       $��14(�6M   
�������$��54,5,253,24q�6n   $��CharacterEllipsis=�6�   ��60   �� *ProgramName+6�   �� 5'      -   5        $��ItemDescription��5'       $��12(�68   
�������$��54,25,253,0q�6Y    �� TextAlignment$
 Left=�6�   $	��282��6�   $��WordEllipsis=�6�   ��6o   �� *ProgramDescription+6�   � 5(      $��336,5,249,5q�6   
�������$	 0.5Q�6C   '  !MaterialDesignThemes.Wpf.PackIcon  5)         Kind$ MicrosoftWindows=�6.   $��	343,8,0,0q�6F   ���	#FF3390BB6Y   �� 5*      -   5        $��ItemWindows��5*       
�������$��361,10,112,21q�6b   $��12(�6y   ��63   �� *	ProgramOS+6y   �� 5+      -   5        $��ItemBit��5+       
�������$��361,27,112,7q�6c   $��12(�6y   ��6/   �� *ProgramOS32OR64+6y   � 5,      $��479,5,106,5q�6   
�������$	 0.5Q�6C   �� 5-      -   5        $��ItemSize��5-       
�������$��485,8,67,23q�6C   $��15(�6X   $ Center=�6�   ��6f   �� *ProgramSize+6�   �� 5.      -	   5        $��ItemSizeByMB��5.       $��MB��64   
�������$��485,25,67,7q�6Q   $��10(�6f   $ Center=�6t   � 5/      $��
524,5,61,5q�6   
�������$	 0.5Q�6B   50      51      52      �� 53   
   -
   5         ��Name$ 
ItemButton��53      #��[ 6%   $��532,7,7,7.5q�6V   
������� � Cursor$
 Hand��6�   ��6~   Z�  6�   �� *ButtonToolTip+.��54        $��23��6<   $��23��6H   $
 Hand��6S    6&   �� *Icon+6S   55      z�56   
   T� 57       T� Y$ 30Q�6!   5:      5<      