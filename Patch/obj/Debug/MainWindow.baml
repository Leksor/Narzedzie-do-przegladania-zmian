   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   @ <Patch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PatchPatch 5   	   3clr-namespace:Patch.Views.HelpPatch.Views.Help 5   	   5clr-namespace:Patch.Views.PatchPatch.Views.Patch 5	   	   5clr-namespace:Patch.Views.PatchPatch.Views.Patch 5
   	   3clr-namespace:Patch.ViewModelsPatch.ViewModels 5   	      Patch.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   %localclr-namespace:Patch     5   	   3helpViewclr-namespace:Patch.Views.Help     5   	   5	patchViewclr-namespace:Patch.Views.Patch     5	   	   8settingsViewclr-namespace:Patch.Views.Patch     5
   	   5
viewModelsclr-namespace:Patch.ViewModels     5   	     � Title$  Patcher��5   	   $	��600��6   $	��900��6)    � 	Resources 5       �� DataType- 'Patch.ViewModels.GeneratePatchViewModel)  #Patch.Views.Patch.GeneratePatchView3  -Patch.ViewModels.GeneratePatchFolderViewModel/  )Patch.Views.Patch.GeneratePatchFolderView( "Patch.ViewModels.LayPatchViewModel$  Patch.Views.Patch.LayPatchView. (Patch.ViewModels.LayPatchFolderViewModel*  $Patch.Views.Patch.LayPatchFolderView/	 )Patch.ViewModels.RollbackChangesViewModel+
  %Patch.Views.Patch.RollbackChangesView5 /Patch.ViewModels.RollbackChangesFolderViewModel1  +Patch.Views.Patch.RollbackChangesFolderView$ Patch.ViewModels.HelpViewModel  Patch.Views.Help.HelpView �� 
TargetType �� Property �� Value �� 
TargetType J� SnapsToDevicePixels �� Triggers	 P� Property
 P� Value �� 
TargetName��%�  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5!      *, +)(�� �     5&      *,	 +)(�� �     5*      *, +)(�� ,    5/      *, +)'�� ^    �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5!   
   "  6   .R�5"        5#      �� 5&   
   " 	 6   .R�5'      
  5(      �� 5*   
   "  6   .R�5+        5,      �� 5/   
   "  6   .R�50        51      �� 54   
   " ��6   .�55      ��  � ��$	 193��6&   �� 56       �@�FocusVisualStyle 61   Z� �� 57       � �� �#ffffff6+   �� 58       � �� �#cccccc6,   �� 59       � ��$ 1q�60   �� 5:       � �� �#3333336+   �� 5;       � ��$ Center=�6;   �� 5<       � ��$ Center=�69   �� 5=       � ��$ 	16,3,16,3q�6(   �� 5>       � �� 5?      �� 5@      " ��6&   .R�5A      �� $��Chrome��6!   #��z��5B   !   #��z��5C   !   #��z��5D   !    . ._�5F      �� $��	Presenter��6/   #��z��6@   #��z��5G   %   #��z��5H   %   ��. # z 5J   %   5K       5L      P� 5M      	 � {�
 .  . �5N   "   ��  � �� �#3333336?   5O      P� 5P      	 �@J�IsMouseOver
 . . �5Q   "   ��  � �� �#dddddd6?   �� 5R   "    � �� �#cccccc6@   �� 5S   "    � �� �#3333336?   5T      P� 5U      	 � ��
 . . �5V   "   ��  � �� �#1ba1e26?   �� 5W   "    � �� �#1ba1e26@   �� 5X   "    � �� �#ffffff6?   5Y      P� 5Z      	 �@J�	IsFocused
 . . �5[   "   �� $ Chrome��6)    � �� �#1ba1e26T   5\      5^      5`      5a      5b      .��5e      ]� .+�5g   
   �  � ColumnDefinitions 5h      �� 5i      $	��205 �6#   61   .+�5k      �� $
��Auto=�6    5l       �� Path �� RelativeSource �� AncestorType��%�   '9�       �� 5m      " 9�6   .�5n      ��  �@9�
IsExpanded 5o      �� $ 
IsSelected� �� " ��5p      5q      ��5r      �� 5s      " ��6&   .R�5t      �� 5u      ��5w      �� 5x      " ��6   .�5y      ��  � �� 5z      �� 5{   "   " ��62   .R�5|   &   �� #��z��67   5}   #   5      5�      .@�5�      9� 
����d���5�      �� 5�       �� Bullet 5�      �� 5�   "   $��64��6(   $��64��63   $
��Left=�6?   $	��Top=�6Z   ._�5�      �� $��10,0,0,0q�6(   $��Generowanie łatki��6:   $��Center=�6S   $��Stretch=�6n   
�������6�   5�      .��5�      � .+�5�      �� ��6'   �� *GeneratePatchViewModels+��5�      �� 5�   "   .R�5�   &   �� $	��2,5q�5�   )   ��5�   -   �� *Name+��5�   )   �� *DataContext.ChangePageCommand+ �� " ���5�   )   �� 5�   )   5�   #   5�      5�      5�      9� 5�      
����d���5�      �� 5�       5�      �� 5�   "   $��64��6(   $��64��63   $
��Left=�6?   $	��Top=�6Z   ._�5�      �� $��10,0,0,0q�6(   $��Nakładanie łatki��6:   $��Center=�6R   $��Stretch=�6m   
�������6�   5�      .��5�      � .+�5�      �� ��6'   �� *LayPatchViewModels+��5�      �� 5�   "   .R�5�   &   �� $	��2,5q�5�   )   ��5�   -   �� *Name+��5�   )   �� *DataContext.ChangePageCommand+ �� " ���5�   )   �� 5�   )   5�   #   5�      5�      5�      9� 5�      
����d���5�      �� 5�       5�      �� 5�   "   $��64��6(   $��64��63   $
��Left=�6?   $	��Top=�6Z   ._�5�      �� $��10,0,0,0q�6(   $��Przywracanie zmian��6:   $��Center=�6T   $��Stretch=�6o   
�������6�   5�      .��5�      � .+�5�      �� ��6'   �� *RollbackViewModels+��5�      �� 5�   "   .R�5�   &   �� $	��2,5q�5�   )   ��5�   -   �� *Name+��5�   )   �� *DataContext.ChangePageCommand+ �� " ���5�   )   �� 5�   )   5�   #   5�      5�      5�      9� 5�      
����d���5�      �� 5�       5�      �� 5�   "   $��64��6(   $��64��63   $
��Left=�6?   $	��Top=�6Z   ._�5�      �� $��10,0,0,0q�6(   $��Pomoc��6:   $��Center=�6G   $��Stretch=�6b   
�������6�   5�      .��5�      � .+�5�      �� ��6'   �� *HelpViewModels+��5�      �� 5�   "   .R�5�   &   �� $	��2,5q�5�   )   ��5�   -   �� *Name+��5�   )   �� *DataContext.ChangePageCommand+ �� " ���5�   )   �� 5�   )   5�   #   5�      5�      5�      5�      5�      �� 5�   
   ��6   �� *CurrentPageViewModel+5     5     