   M S B A M L   `   `   `  ����@  <Patch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Patch.AnimationsPatch.Animations  5   	   5clr-namespace:Patch.Views.PatchPatch.Views.Patch  5   	   *    $Patch.Views.Patch.LayPatchFolderView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   4	animationclr-namespace:Patch.Animations     5   	   1localclr-namespace:Patch.Views.Patch     5   	   .��5
      �   � 	Resources  5   
      	BoolToVis  HeaderStyle �� 
TargetType �� Property �� Value��%�   &	        &	      �� 5      6+   �� 5      " ��6(   .�5      ��  � ��$ Times New Roman)�6/   �� 5       � �� �	#FF0097FF6/   �� 5       � ��$ 40(�6-   5      5       � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��3* �6   �� 5      $��3* �6   �� 5      $��3* �6   .+�5!   
   �� #��[ 6   $��0��69   $��Nałóż Łatkę��6F   $��Center=�6Y   $��Center=�6v   � 5$   
   $��1��6    � ColumnDefinitions 5%      �� 5&      $��3* �6#   �� 5'      $��* �6#   �� 5(      $��6* �6#   .+�5,      � $��0��6    5.      �� 5/      $��* �6$   �� 50      $��* �6$   �� 51      $��* �6$   .+�56      �� -   5        $��ChooseSecondFile��59      
����ح�$��0��56   V   $��0��6f   $��Wczytaj folder��58      $��33��62   $��Center=�6>    � Cursor$
 Hand��6[   $��Center=�59   1   $	��120��6L   ��56   0   �� *LoadFolderCommand+{�57      ��  �� Path$ IsLayPatchButtonEnable� �� UpdateSourceTrigger$ PropertyChanged=�59   L   �� 5<      
����ح�$��0��6W   $��1��6g   $��Resetuj folder��5?      $��33��62   $��Center=�6>   $
 Hand��6[   $��Center=�5@      $	��120��64   ��5<   0   �� *ResetFolderCommand+{�5=      �� $ IsLayPatchButtonEnable�$ PropertyChanged=�y�5>      �� $ IsFolderLoaded�	 �� 	Converter#	 [  $ PropertyChanged=�
 �� Mode$
 TwoWay=�5@   4   �� 5C      
����ح�$��0��60   $��2��6@   $
 Hand��6M   $��Podejrzyj Pliki��5G      $��30��63   $��Center=�5H      $��Center=�66   $	��120��5I      ��5D      �� *PreviewFolderCommand+{�5E      �� $ IsLayPatchButtonEnable�$ PropertyChanged=�y�5F      �� $ IsFolderLoaded�#	 [  $ PropertyChanged=�$
 TwoWay=�5I      5J      �� 5M      $��1��6   $(��"/Patch;component/Images/folder.png��6$   y�5N      �� $ IsFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�� 5Q      -   5        $��
FirstStack��5Q      $��2��6%    5T      �� 5U      $��* �6$   �� 5V      $��* �6$   �� 5W      $��* �6$    5[      �� 5\      $��2* �6'   �� 5]      $��5* �6'   .+�5`      �� $��Center=�6   $��0��5a      $��0��6%   $��Nazwa:��65   y�5`   3   �� $ IsFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5a   5   �� 5b      $��Center=�6   $��0��5c      $��1��6%   y�5b   3   �� $ IsFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5c   5   �� $ FolderInfo.Name��� 5f      $��Center=�6   $��1��5g      $��0��6%   $��
Ścieżka:��65   y�5f   3   �� $ IsFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5g   5   �� 5h      $��Center=�6   $��1��5i      $��1��6%   y�5h   3   �� $ IsFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5i   5   �� $ FolderInfo.FullName��� 5l      $��Center=�6   $��2��5m      $��0��6%   $��Data utworzenia:��65   y�5l   3   �� $ IsFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5m   5   �� 5n      $��Center=�6   $��2��5o      $��1��6%   y�5n   3   �� $ IsFolderLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5o   5   �� $ FolderInfo.CreationTime�5q      5r      � 5u   
   $��2��6    5v      �� 5w      $��3* �6#   �� 5x      $��* �6#   �� 5y      $��6* �6#   .+�5}      � $��0��6    5~      �� 5      $	��58* �6'   �� 5�      $	��47* �6'    5�      �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$   .+�5�      �� -   5        $��ChooseFirstFile��5�      
����ح�$��0��5�   U   $��Wczytaj łatkę��5�      $��33��61   $��Center=�6=   $
 Hand��6Z   $��Center=�5�   0   $	��120��6K   $��2��6W   $��45,11q�6k   ��5�   0   �� *LoadPatchCommand+{�5�      �� $ IsLayPatchButtonEnable�$ PropertyChanged=�5�   k   �� 5�      
����ح�$��0��6V   $��1��6f   $��Resetuj łatkę��5�      $��32��61   $��Center=�6=   $
 Hand��6Z   $��Center=�5�      $	��120��64   $��2��6@   $��45,11q�6T   ��5�   0   �� *ResetPatchCommand+{�5�      �� $ IsLayPatchButtonEnable�$ PropertyChanged=�y�5�      �� $ IsPatchLoaded�#	 [  $ PropertyChanged=�$
 TwoWay=�5�   T   �� 5�      
����ح�$��0��60   $��2��6@   $
 Hand��6M   $��Podejrzyj łatkę��5�      $��30��63   $��Center=�5�      $��Center=�66   $	��120��5�      $��2��6%   $��45,13,45,12q�69   ��5�      �� *PreviewPatchCommand+{�5�      �� $ IsLayPatchButtonEnable�$ PropertyChanged=�y�5�      �� $ IsPatchLoaded�#	 [  $ PropertyChanged=�$
 TwoWay=�5�   9   5�      �� 5�      $��1��6   $&�� /Patch;component/Images/file.png��6$   y�5�      �� $ IsPatchLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�� 5�      -   5        $��
PatchStack��5�      $��2��6%    J� 	AllowDrop .  5�      �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$   �� 5�      $��* �6$    5�      �� 5�      $��2* �6'   �� 5�      $��5* �6'   .+�5�      �� $��0��5�      $��0��6%   $��Nazwa:��65   y�5�      �� $ IsPatchLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5�   5   �� 5�      $��0��5�      $��1��6%   y�5�      �� $ IsPatchLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5�   5   �� $ PatchInfo.Name��� 5�      $��1��5�      $��0��6%   $��Rozszerzenie:��65   y�5�      �� $ IsPatchLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5�   5   �� 5�      $��1��5�      $��1��6%   y�5�      �� $ IsPatchLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5�   5   �� $ PatchInfo.Extension��� 5�      $��2��5�      $��0��6%   $��Rozmiar:��65   y�5�      �� $ IsPatchLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5�   5   �� 5�      $��2��5�      $��1��6%   y�5�      �� $ IsPatchLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5�   5   �� $ PatchInfo.Size��� 5�      $��3��5�      $��0��6%   $��Data utworzenia:��65   y�5�      �� $ IsPatchLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5�   5   �� 5�      $��3��5�      $��1��6%   y�5�      �� $ IsPatchLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5�   5   �� $ PatchInfo.CreateDate��� 5�      $��4��5�      $��0��6%   $��Katalog:��65   y�5�      �� $ IsPatchLoaded�#	 [  $ PropertyChanged=�$
 OneWay=�5�   5   �� 5�      $��4��5�      $��1��6%   y�5�      �� $ IsPatchLoaded�#	 [  $ PropertyChanged=�$
 OneWay=���5�   5   �� $ PatchInfo.Directory��� 5�      $��2��6   $��1��6%   $7��1Wczytaj plik, lub przeciągnij go w to miejsce...��65   y�5�      �� $ IsPatchLoadedNegation�#	 [  $ PropertyChanged=�$
 OneWay=�5�      5�      � 5�   
   $��3��6   $��0, 20, 0, 0q�6    5�      �� 5�      $��3* �6#   �� 5�      $��3* �6#   �� 5�      $��4* �6#   .+�5�      �� 
����ح�$��0��6,   $��0��69   $
 Hand��6I   $��Nałóż Łatkę��5�      $��30��6/   $	��120��5�      ��5�      �� *LayPatchFolderCommand+{�5�      �� $ IsLayPatchButtonEnable�$ PropertyChanged=�y�5�      �� $ IsLayPatchButtonVisible�#	 [  $ PropertyChanged=�5�      � 5�      $��1��6    5�      �� 5�      $��5* �6$   �� 5�      $��2* �6$   .+�5�      *   $Patch.Animations.CircularProgressBar  $��0��60   $��1��6=   y�6M   �� $ IsAnimationActive�$ PropertyChanged=�#	 [  6�   �� 5�      $��1��6   $��1��6%   $��Center=�6]   $��Center=�6z   ��65   �� $ LayPatchStatus�6z   5�      � 5�      $��2��6    5�      �� 5�      $��* �6$   �� 5�      $��* �6$   .+�5�      �� 
����ح�$��0��60   $��2��6=   $
 Hand��6M   $��Zaktualizuj plik��5     $��30��64   $��Center=�5     $��Center=�66   $	��130��5     ��5�      �� *SaveGeneratedFileCommand+y�5      �� $ IsDownPanelAvaiable�#	 [  $ PropertyChanged=�$
 TwoWay=�5     �� 5     
����ح�$��1��60   $
 Hand��6=   $��Podejrzyj zmiany��5	     $��30��64   $��Center=�5
     $��Center=�66   $	��130��5     ��5     �� *PreviewGeneratedFileCommand+y�5     �� $ IsDownPanelAvaiable�#	 [  $ PropertyChanged=�$
 TwoWay=�5     5     5     5     5     