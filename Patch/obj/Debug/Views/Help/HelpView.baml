   M S B A M L   `   `   `  ����@  <Patch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Patch.Views.HelpPatch.Views.Help  5          Patch.Views.Help.HelpView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Patch.Views.Help     5      .��5	      �   � ColumnDefinitions  5   
   �� 5      $	��200 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��* �6    � 	Resources 5   
      HeaderStyle �� 
TargetType �� Property �� Value��%�   &	        �� 5      " ��6(   .�5      ��  � ��$ Algerian)�6/   �� 5       � �� �	#FF0097FF6/   �� 5       � ��$ 70(�6-   5      5      .+�5   
   �� 
����ح�$��0��6(   $��2��68   $��Generowanie łatki��5       $��	25,20,0,0q�5!      $��30��6$   $
��Left=�5"      $	��Top=�6,   $	��150��5#      ��5      �� *ShowGeneratePatchCommand+5#      �� 5%   
   
����ح�$��0��6(   $��2��68   $��Nakładanie Łatki��5'      $��	25,70,0,0q�5(      $��30��6$   $
��Left=�5)      $	��Top=�6,   $	��150��5*      ��5&      �� *ShowLayPatchCommand+5*      �� 5,   
   
����ح�$��0��6(   $��2��68   $��Cofanie zmian��5.      $��
25,120,0,0q�5/      $��30��6%   $
��Left=�50      $	��Top=�6,   $	��150��51      ��5-      �� *ShowShareHelpCommand+51      �� 53   
   #��[  6   $��1��69   $��0��6I   $��POMOC��6V   $	��100��6c   $��Center=�6p   $��Center=�6�   (  "System.Windows.Controls.WebBrowser  54   
   -   5        $��
WebBrowser��54      $��1��6'   $
��Left=�67   $
��Auto��6R   $��1��6`   $	��Top=�6m   $
��Auto��6�   55      56      