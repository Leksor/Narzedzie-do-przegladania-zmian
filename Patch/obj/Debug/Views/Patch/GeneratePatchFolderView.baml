   M S B A M L   `   `   `  ����@  <Patch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Patch.AnimationsPatch.Animations  5      5clr-namespace:Patch.Views.PatchPatch.Views.Patch  5      /    )Patch.Views.Patch.GeneratePatchFolderView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4	animationclr-namespace:Patch.Animations     5      1localclr-namespace:Patch.Views.Patch     5      .��5
      �   � 	Resources  5   
      	BoolToVis  HeaderStyle �� 
TargetType �� Property �� Value��%�   &	        &	      �� 5      6+   �� 5      " ��6(   .�5      ��  � ��$ Times New Roman)�6/   �� 5       � �� �	#FF0097FF6/   �� 5       � ��$ 40(�6-   5      5       � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��3* �6   �� 5      $��3* �6   �� 5      $��3* �6   .+�5!   
   �� #��[ 6   $��0��69   $��Generuj Łatkę��6F   $��Center=�6[   $��Center=�6x   � 5$   
   $��1��6    � ColumnDefinitions 5%      �� 5&      $��3* �6#   �� 5'      $��2* �6#   �� 5(      $��5* �6#   .+�5,      � $��0��6    5.      �� 5/      $��* �6$   �� 50      $��* �6$   �� 51      $��* �6$   .+�56      �� -   5        $��ChooseSecondFile��59      
����ح�$��0��56   [   $��0��6k   $��Wczytaj folder��58      $��33��62   $��Center=�6>    � Cursor$
 Hand��6[   $��Center=�59   1   $	��120��6L   ��56   0   �� *LoadFirstFolderCommand+{�57      ��  �� Path$! IsGeneratePatchButtonEnable� �� UpdateSourceTrigger$ PropertyChanged=�59   L   �� 5<      
����ح�$��0��6\   $��1��6l   $��Resetuj folder��5?      $��33��62   $��Center=�6>   $
 Hand��6[   $��Center=�5@      $	��120��64   ��5<   0   �� *ResetFirstFolderCommand+{�5=      �� $! IsGeneratePatchButtonEnable�$ PropertyChanged=�y�5>      �� $ IsFirstFolderLoaded�	 �� 	Converter#	 [  $ PropertyChanged=�
 �� Mode$
 TwoWay=�5@   4   �� 5C      
����ح�$��0��60   $��2��6@   $
 Hand��6M   $��Podejrzyj Pliki��5G      $��30��63   $��Center=�5H      $��Center=�66   $	��120��5I      ��5D      �� *PreviewFirstFolderCommand+{�5E      �� $! IsGeneratePatchButtonEnable�$ PropertyChanged=�y�5F      �� $ IsFirstFolderLoaded�#	 [  $ PropertyChanged=�$
 TwoWay=�5I      5J      �� 5M      $��1��6   $(��"/Patch;component/Images/folder.png��6$   y�5N      �� $ IsFirstFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�� 5Q      -   5        $��
FirstStack��5Q      $��2��6%    5T      �� 5U      $��* �6$   �� 5V      $��* �6$   �� 5W      $��* �6$    5[      �� 5\      $��2* �6'   �� 5]      $��5* �6'   .+�5a      �� $��0��5b      $��0��6%   $��Nazwa:��65   $��Center=�6F   y�5a      �� $ IsFirstFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5b   F   �� 5c      $��0��5d      $��1��6%   $��Center=�6c   y�5c      �� $ IsFirstFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5d   5   �� $ FirstFolderInfo.Name�6c   �� 5g      $��1��5h      $��0��6%   $��
Ścieżka:��65   $��Center=�6H   y�5g      �� $ IsFirstFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5h   H   �� 5i      $��1��5j      $��1��6%   $��Center=�6g   y�5i      �� $ IsFirstFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5j   5   �� $ FirstFolderInfo.FullName�6g   �� 5m      $��2��5n      $��0��6%   $��Data utworzenia:��65   $��Center=�6P   y�5m      �� $ IsFirstFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5n   P   �� 5o      $��2��5p      $��1��6%   $��Center=�6k   y�5o      �� $ IsFirstFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5p   5   �� $" FirstFolderInfo.CreationTime�6k   5r      5s      � 5v   
   $��2��6    5w      �� 5x      $��3* �6#   �� 5y      $��2* �6#   �� 5z      $��5* �6#   .+�5~      � $��0��6    5�      �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$   .+�5�      �� -   5        $��ChooseFirstFile��5�      
����ح�$��0��5�   \   $��Wczytaj folder��5�      $��33��62   $��Center=�6>   $
 Hand��6[   $��Center=�5�   0   $	��120��6K   ��5�   0   �� *LoadSecondFolderCommand+{�5�      �� $! IsGeneratePatchButtonEnable�$ PropertyChanged=�5�   K   �� 5�      
����ح�$��0��6]   $��1��6m   $��Resetuj folder��5�      $��33��62   $��Center=�6>   $
 Hand��6[   $��Center=�5�      $	��120��64   ��5�   0   �� *ResetSecondFolderCommand+{�5�      �� $! IsGeneratePatchButtonEnable�$ PropertyChanged=�y�5�      �� $ IsSecondFolderLoaded�#	 [  $ PropertyChanged=�$
 TwoWay=�5�   4   �� 5�      
����ح�$��0��60   $��2��6@   $
 Hand��6M   $��Podejrzyj Pliki��5�      $��30��63   $��Center=�5�      $��Center=�66   $	��120��5�      ��5�      �� *PreviewSecondFolderCommand+{�5�      �� $! IsGeneratePatchButtonEnable�$ PropertyChanged=�y�5�      �� $ IsSecondFolderLoaded�#	 [  $ PropertyChanged=�$
 TwoWay=�5�      5�      �� 5�      $��1��6   $(��"/Patch;component/Images/folder.png��6$   y�5�      �� $ IsSecondFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�� 5�      -   5        $��SecondStack��5�      $��2��6&    5�      �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$    5�      �� 5�      $��2* �6'   �� 5�      $��5* �6'   .+�5�      �� $��0��5�      $��0��6%   $��Nazwa:��65   $��Center=�6F   y�5�      �� $ IsSecondFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5�   F   �� 5�      $��0��5�      $��1��6%   $��Center=�6d   y�5�      �� $ IsSecondFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5�   5   �� $ SecondFolderInfo.Name�6d   �� 5�      $��1��5�      $��0��6%   $��
Ścieżka:��65   $��Center=�6H   y�5�      �� $ IsSecondFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5�   H   �� 5�      $��1��5�      $��1��6%   $��Center=�6h   y�5�      �� $ IsSecondFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5�   5   �� $ SecondFolderInfo.FullName�6h   �� 5�      $��2��5�      $��0��6%   $��Data utworzenia:��65   $��Center=�6P   y�5�      �� $ IsSecondFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5�   P   �� 5�      $��2��5�      $��1��6%   $��Center=�6l   y�5�      �� $ IsSecondFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5�   5   �� $# SecondFolderInfo.CreationTime�6l   5�      5�      � 5�   
   $��3��6   $��0, 20, 0, 0q�6    5�      �� 5�      $��3* �6#   �� 5�      $��3* �6#   �� 5�      $��4* �6#   .+�5�      �� 
����ح�$��0��6,   $��0��69   $
 Hand��6I   $��Generuj Łatkę��5�      $��30��61   $��Center=�5�      $��Center=�66   $	��120��5�      ��5�      �� *GeneratePatchCommand+{�5�      �� $! IsGeneratePatchButtonEnable�$ PropertyChanged=�y�5�      �� $" IsGeneratePatchButtonVisible�#	 [  $ PropertyChanged=�5�      � 5�      $��1��6    5�      �� 5�      $��5* �6$   �� 5�      $��2* �6$   .+�5�      *   $Patch.Animations.CircularProgressBar  $��0��60   $��1��6=   y�6M   �� $ IsAnimationActive�$ PropertyChanged=�#	 [  6�   �� 5�      $��1��6   $��1��6%   $��Center=�6b   $��Center=�6   ��65   �� $ GeneratePatchStatus�6   5�      � 5�      $��2��6    5�      �� 5�      $��* �6$   �� 5�      $��* �6$   .+�5�      �� 
����ح�$��0��60   $��2��6=   $
 Hand��6M   $��Podejrzyj Łatkę��5�      $��30��63   $��Center=�5�      $��Center=�66   $	��130��5�      ��5�      �� *PreviewPatchCommand+y�5�      �� $ IsDownPanelAvaiable�#	 [  $ PropertyChanged=�$
 TwoWay=�5�      �� 5�      
����ح�$��1��60   $��2��6=   $
 Hand��6M   $��Zapisz Łatkę��5�      $��30��60   $��Center=�5�      $��Center=�66   $	��130��5�      ��5�      �� *SavePatchCommand+y�5�      �� $ IsDownPanelAvaiable�#	 [  $ PropertyChanged=�$
 TwoWay=�5�      5�      5�      5�      5�      