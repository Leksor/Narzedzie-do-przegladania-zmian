   M S B A M L   `   `   `  ����@  <Patch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Patch.AnimationsPatch.Animations  5      5clr-namespace:Patch.Views.PatchPatch.Views.Patch  5      )    #Patch.Views.Patch.GeneratePatchView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4	animationclr-namespace:Patch.Animations     5      1localclr-namespace:Patch.Views.Patch     5      .��5
      �   � 	Resources  5   
      	BoolToVis  HeaderStyle �� 
TargetType �� Property �� Value��%�   &	        &	      �� 5      6+   �� 5      " ��6(   .�5      ��  � ��$ Times New Roman)�6/   �� 5       � �� �	#FF0097FF6/   �� 5       � ��$ 40(�6-   5      5       � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��3* �6   �� 5      $��3* �6   �� 5      $��3* �6   .+�5!   
   �� #��[ 6   $��0��69   $��Generuj Łatkę��6F   $��Center=�6[   $��Center=�6x   � 5$   
   $��1��6    � ColumnDefinitions 5%      �� 5&      $��3* �6#   �� 5'      $��* �6#   �� 5(      $��6* �6#   .+�5,      � $��0��6    5.      �� 5/      $��* �6$   �� 50      $��* �6$   �� 51      $��* �6$   .+�56      �� -   5        $��ChooseSecondFile��59      
����ح�$��0��56   Y   $��0��6i   $��Wczytaj plik��58      $��33��60   $��Center=�6<    � Cursor$
 Hand��6Y   $��Center=�59   1   $	��120��6L   ��56   0   �� *LoadFirstFileCommand+{�57      ��  �� Path$! IsGeneratePatchButtonEnable� �� UpdateSourceTrigger$ PropertyChanged=�59   L   �� 5<      
����ح�$��0��6Z   $��1��6j   $��Resetuj plik��5?      $��33��60   $��Center=�6<   $
 Hand��6Y   $��Center=�5@      $	��120��64   ��5<   0   �� *ResetFirstFileCommand+{�5=      �� $! IsGeneratePatchButtonEnable�$ PropertyChanged=�y�5>      �� $ IsFirstFileLoaded�	 �� 	Converter#	 [  $ PropertyChanged=�
 �� Mode$
 TwoWay=�5@   4   �� 5C      
����ح�$��0��60   $��2��6@   $
 Hand��6M   $��Podejrzyj Plik��5G      $��30��62   $��Center=�5H      $��Center=�66   $	��120��5I      ��5D      �� *PreviewFirstFileCommand+{�5E      �� $! IsGeneratePatchButtonEnable�$ PropertyChanged=�y�5F      �� $ IsFirstFileLoaded�#	 [  $ PropertyChanged=�$
 TwoWay=�5I      5J      �� 5M      $��1��6   $&�� /Patch;component/Images/file.png��6$   y�5N      �� $ IsFirstFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�� 5Q      -   5        $��
FirstStack��5Q      $��2��6%    J� 	AllowDrop .  5T      �� 5U      $��* �6$   �� 5V      $��* �6$   �� 5W      $��* �6$   �� 5X      $��* �6$   �� 5Y      $��* �6$    5]      �� 5^      $��* �6'   �� 5_      $��3* �6'   .+�5b      �� $��0��5c      $��0��6%   $��Nazwa:��65   y�5b      �� $ IsFirstFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5c   5   �� 5d      $��0��5e      $��1��6%   y�5d      �� $ IsFirstFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5e   5   �� $ FirstFileInfo.Name��� 5h      $��1��5i      $��0��6%   $��Rozszerzenie:��65   y�5h      �� $ IsFirstFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5i   5   �� 5j      $��1��5k      $��1��6%   y�5j      �� $ IsFirstFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5k   5   �� $ FirstFileInfo.Extension��� 5n      $��2��5o      $��0��6%   $��Rozmiar:��65   y�5n      �� $ IsFirstFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5o   5   �� 5p      $��2��5q      $��1��6%   y�5p      �� $ IsFirstFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5q   5   �� $ FirstFileInfo.Size��� 5t      $��3��5u      $��0��6%   $��Data utworzenia:��65   y�5t      �� $ IsFirstFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5u   5   �� 5v      $��3��5w      $��1��6%   y�5v      �� $ IsFirstFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5w   5   �� $ FirstFileInfo.CreateDate��� 5z      $��4��5{      $��0��6%   $��Katalog:��65   y�5z      �� $ IsFirstFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5{   5   �� 5|      $��4��5}      $��1��6%   y�5|      �� $ IsFirstFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5}   5   �� $ FirstFileInfo.Directory��� 5�      $��2��6   $��1��6%   $7��1Wczytaj plik, lub przeciągnij go w to miejsce...��65   y�5�      �� $ IsFirstFileLoadedNegation�#	 [  $ PropertyChanged=�$
 OneWay=�5�      5�      � 5�   
   $��2��6    5�      �� 5�      $��3* �6#   �� 5�      $��* �6#   �� 5�      $��6* �6#   .+�5�      � $��0��6    5�      �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$   .+�5�      �� -   5        $��ChooseFirstFile��5�      
����ح�$��0��5�   Z   $��Wczytaj plik��5�      $��33��60   $��Center=�6<   $
 Hand��6Y   $��Center=�5�   0   $	��120��6K   ��5�   0   �� *LoadSecondFileCommand+{�5�      �� $! IsGeneratePatchButtonEnable�$ PropertyChanged=�5�   K   �� 5�      
����ح�$��0��6[   $��1��6k   $��Resetuj plik��5�      $��33��60   $��Center=�6<   $
 Hand��6Y   $��Center=�5�      $	��120��64   ��5�   0   �� *ResetSecondFileCommand+{�5�      �� $! IsGeneratePatchButtonEnable�$ PropertyChanged=�y�5�      �� $ IsSecondFileLoaded�#	 [  $ PropertyChanged=�$
 TwoWay=�5�   4   �� 5�      
����ح�$��0��60   $��2��6@   $
 Hand��6M   $��Podejrzyj Plik��5�      $��30��62   $��Center=�5�      $��Center=�66   $	��120��5�      ��5�      �� *PreviewSecondFileCommand+{�5�      �� $! IsGeneratePatchButtonEnable�$ PropertyChanged=�y�5�      �� $ IsSecondFileLoaded�#	 [  $ PropertyChanged=�$
 TwoWay=�5�      5�      �� 5�      $��1��6   $&�� /Patch;component/Images/file.png��6$   y�5�      �� $ IsSecondFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�� 5�      -   5        $��SecondStack��5�      $��2��6&    .  5�      �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$    5�      �� 5�      $��* �6'   �� 5�      $��3* �6'   .+�5�      �� $��0��5�      $��0��6%   $��Nazwa:��65   y�5�      �� $ IsSecondFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5�   5   �� 5�      $��0��5�      $��1��6%   y�5�      �� $ IsSecondFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5�   5   �� $ SecondFileInfo.Name��� 5�      $��1��5�      $��0��6%   $��Rozszerzenie:��65   y�5�      �� $ IsSecondFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5�   5   �� 5�      $��1��5�      $��1��6%   y�5�      �� $ IsSecondFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5�   5   �� $ SecondFileInfo.Extension��� 5�      $��2��5�      $��0��6%   $��Rozmiar:��65   y�5�      �� $ IsSecondFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5�   5   �� 5�      $��2��5�      $��1��6%   y�5�      �� $ IsSecondFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5�   5   �� $ SecondFileInfo.Size��� 5�      $��3��5�      $��0��6%   $��Data utworzenia:��65   y�5�      �� $ IsSecondFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5�   5   �� 5�      $��3��5�      $��1��6%   y�5�      �� $ IsSecondFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5�   5   �� $ SecondFileInfo.CreateDate��� 5�      $��4��5�      $��0��6%   $��Katalog:��65   y�5�      �� $ IsSecondFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5�   5   �� 5�      $��4��5�      $��1��6%   y�5�      �� $ IsSecondFileLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5�   5   �� $ SecondFileInfo.Directory��� 5�      $��2��6   $��1��6%   $7��1Wczytaj plik, lub przeciągnij go w to miejsce...��65   y�5�      �� $  IsSecondFileLoadedNegation�#	 [  $ PropertyChanged=�$
 OneWay=�5�      5�      � 5�   
   $��3��6   $��0, 20, 0, 0q�6    5�      �� 5�      $��3* �6#   �� 5�      $��3* �6#   �� 5�      $��4* �6#   .+�5�      �� 
����ح�$��0��6,   $��0��69   $
 Hand��6I   $��Generuj Łatkę��5�      $��30��61   $��Center=�5�      $��Center=�66   $	��120��5�      ��5�      �� *GeneratePatchCommand+{�5�      �� $! IsGeneratePatchButtonEnable�$ PropertyChanged=�y�5�      �� $" IsGeneratePatchButtonVisible�#	 [  $ PropertyChanged=�5�      � 5�      $��1��6    5�      �� 5�      $��5* �6$   �� 5�      $��2* �6$   .+�5     *   $Patch.Animations.CircularProgressBar  $��0��60   $��1��6=   y�6M   �� $ IsAnimationActive�$ PropertyChanged=�#	 [  6�   �� 5     $��1��6   $��1��6%   $��Center=�6b   $��Center=�6   ��65   �� $ GeneratePatchStatus�6   5     � 5     $��2��6    5     �� 5     $��* �6$   �� 5	     $��* �6$   �� 5
     $��* �6$   .+�5     �� 
����ح�$��0��60   $��2��6=   $
 Hand��6M   $��Podejrzyj Zmiany��5     $��30��64   $��Center=�5     $��Center=�66   $	��130��5     ��5     �� *PreviewFilesChangeCommand+y�5     �� $ IsDownPanelAvaiable�#	 [  $ PropertyChanged=�$
 TwoWay=�5     �� 5     
����ح�$��1��60   $��2��6=   $
 Hand��6M   $��Podejrzyj Łatkę��5     $��30��63   $��Center=�5     $��Center=�66   $	��130��5     ��5     �� *PreviewPatchCommand+y�5     �� $ IsDownPanelAvaiable�#	 [  $ PropertyChanged=�$
 TwoWay=�5     �� 5     
����ح�$��2��60   $��2��6=   $
 Hand��6M   $��Zapisz Łatkę��5!     $��30��60   $��Center=�5"     $��Center=�66   $	��130��5#     ��5     �� *SavePatchCommand+y�5      �� $ IsDownPanelAvaiable�#	 [  $ PropertyChanged=�$
 TwoWay=�5#     5%     5&     5'     5(     