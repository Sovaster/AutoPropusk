   M S B A M L   `   `   `  ����F  BAutoPropusk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AutoPropuskAutoPropusk  5   	       AutoPropusk.Cars   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   %localclr-namespace:AutoPropusk  5   	   -   5          � WindowStartupLocation$  CenterScreen=�5	   	    � SizeToContent$ Height=�5
   	   $
��1000��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency . ���#BE008A��5      �� 5   
    �� 
TargetType" �6   .R�5      �� $��	newBorder��6    �� CornerRadius$ 30��6&   ���#BE008A._�5      � .+�5      �� $��1��6   ._�5      �� .u�5      �� 5      5      �� 5      $��2��6!   $��Right=�6.   $��Bottom=�6J   $y�	Collapsed=�5      ��.  64   5      5      5      .��5       � .+�5!   
   �� 
�������$��10 10q�6)   $ 30��68   ._�5"      �  � ColumnDefinitions 5#      �� 5$      �� 5%      �� 5&      �� 5'       � RowDefinitions 5)      �� 5*      �� 5+      �� 5,      �� 5-      �� 5.      �� 5/      �� 50      .+�53      ��     MaterialDesignBody1TextBlock#��[  6   $��0��6R   $��1��6_   $��2��6o   $��Машины��6�   $��Center=�6�   $��Center=�6�   �� 54      -   5        	 ��Name$	 BackBTN��54      $��10 10q�6*   $	��150��69   $��
Назад��6E   $��0��6U   $��3��6b   ���#BE008A+  %MaterialDesignThemes.Wpf.ButtonAssist
   CornerRadius$
 15��55   !   $��Right=�6O   ���#7C005A$��2q�6�   
���   �6�   �� 58      -   5         ��Name$ TbNomer��5=      $��10q�58      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 
Номер��59         
Foreground �#015965.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush �#015965   HasClearButton . $��1��5>      $��0��6+    ! MaterialDesignOutlinedTextBox#��[ 5A      �� 5B      -   5        $ TbGod��5G      $��10q�5B      $ Год��5C       �#015965 �#015965 . $��1��5H      $��2��6+   #��[ 5K      �� 5N      -   5         ��Name$ CbMark��5R      $��10q�5N       �#015965 �#015965$��50��5Q      $ 
Марка��5S      $��1��5T      $��1��6&    " MaterialDesignOutlinedComboBox#��[ 5U      �� 5V      -   5        $ CbClient��5Z      $��10q�5V       �#015965 �#015965$��50��5Y      $ Клиент��5[      $��1��5\      $��3��6&   #��[ 5]      �� 5`      -   5        $	 InsertMarkBTN��5`      $��10 10q�60   $��Добавить��6?   $��4��6R   $��3��6_   ���#BE008A$
 15��5a   !   ���#7C005A$��2q�6e   
���   �6y   �� 5b      -   5        $	 DeleteMarkBTN��5b      $��10 10q�60   $��Удалить��6?   $��4��6Q   $��0��6^   ���#BE008A$
 15��5c   !   ���#7C005A$��2q�6e   
���   �6y   �� 5d      -	   5        $	 EditMarkBTN��5d      $��10 10q�6.   $��Изменить��6=   $��4��6P   $��2��6]   ���#BE008A$
 15��5e   !   ���#7C005A$��2q�6e   
���   �6y   &   System.Windows.Controls.DataGrid  5g      -
   5          Name$ GridCar��5g   D   $��10q�6   $��Center=�6'   $��0��6U   $��4��6e   $��5��6y   $��2��6�     MaterialDesignDataGrid#��[ 5h      5i      5j      5k      5l      