   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   @ <Patch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Patch.ViewsPatch.Views 5   	   #   Patch.Views.PreviewTreeFolder   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   +localclr-namespace:Patch.Views     5   	     � Title$  Podgląd plików��5	   	   $	��600��6    $	��500��6-   .��5      �  � 	Resources 5   
      	BoolToVis��%   &	        �� 5      6+   5      .+�5   
   �� 
�������$��1q�6$   ._�5      �� $
��Auto=�6   .��5      R� ��6   �� *TreeCollection+��5      �� 5       �� 
TargetType" Q�6    .�5      ��  �� Property �@Q�
IsExpanded �� Value . 6;   5      ��5      �� 5       �� ItemsSource 63   �� *TreeCollection+.R�5      � $��
0, 5, 0, 5q�6#    � ColumnDefinitions 5   "   �� 5   &   $��* �67   �� 5   &   $��* �67   �� 5   &   $��* �67   .+�5!   "   �� $��0��6,   $��Center=�6R   $��0, 0, 20, 0q�6m   ��6<   �� *Name+��5"   &   �� 5#   *   " ��60    �� Triggers 5$   .   �� 5%   2    �� Binding 6>   �� *	IsChanged+	 �� Value	 True6\   .a�5&   6   ��  � �� �#FFF74F6S   5'   3   5)   +   5+   #   �� 5-   "   $��1��6)   
����ح�
 � Cursor$

 Hand��5.   @   $��Podejrzyj Plik��6r   $��25��6�   $��Center=�5/   )   $��Center=�6F   $	��100��6a   y�5-   9   ��  �� Path$ IsFile� �� 	Converter# [   �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ OneWay=���5.   N   �� *ShowFileCommand+5/   a   �� 51   "   $��
8, 0, 0, 0q�6)   $��2��6=   
����ح�$

 Hand��52   @   $��Podejrzyj Zmiany��6r   $��25��6�   $��Center=�53   )   $��Center=�6F   $	��120��6a   y�51   M   �� $ 	IsChanged�# [  $ PropertyChanged=�$ OneWay=���52   N   �� *ShowFileChanges+53   a   54      55      57      58      59      5:      5;      