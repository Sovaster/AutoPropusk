   M S B A M L   `   `   `  ����F  BAutoPropusk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AutoPropuskAutoPropusk  5   	       AutoPropusk.Authorization   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   %localclr-namespace:AutoPropusk  5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � SizeToContent$ Height=�5
   	   $	��300��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency . ���#BE008A��5      �� 5   
    �� 
TargetType" �6   .R�5      �� $��	newBorder��6    �� CornerRadius$ 30��6&   ���#BE008A._�5      � .+�5      �� $��1��6   ._�5      �� .u�5      �� 5      5      �� 5      $��2��6!   $��Right=�6.   $��Bottom=�6J   $y�	Collapsed=�5      ��.  64   5      5      5      .��5      � .+�5    
   �� .+�5!      �� 
�������$��10 10q�6-   $ 30��6<   ._�5"      �� $��25q�6   .+�5#      ��     MaterialDesignBody1TextBlock#��[  6    $��Center=�6V   $��AutoPropusk��6s   �� 5$      -   5         ��Name$ TbLogin��5)      $��15q�5$      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 
Логин��5%      	   
Foreground	 �#015965.  (MaterialDesignThemes.Wpf.TextFieldAssist
   UnderlineBrush
 �#015965   HasClearButton .  ! MaterialDesignOutlinedTextBox#��[ 5+      G� 5,      -   5        $��
PbPassword��51   "   $��15q�5,   "   $ Пароль��5-   "   	 �#015965
 �#015965 .  + 'MaterialDesignOutlinedRevealPasswordBox#��[ 53   "   �� 54       �� Orientation$ Vertical=�6!   $��Center=�68   .+�55      �� -   5         ��Name$ EnterBTN��55   !   $��10 10q�63   $��
Войти��6B   ���#BE008A+  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 15��56   !   ���#7C005A$��2q�6e   
���   �6y   �� 57      -   5        $ BackBTN��57   !   $��10 10q�62   $
��БД��6A   ���#BE008A$ 15��58   !   ���#7C005A$��2q�6e   
���   �6y   �� 59      -   5        $ ExitBTN��59   !   $��10 10q�62   $��
Выход��6A   ���#BE008A$ 15��5:   !   ���#7C005A$��2q�6e   
���   �6y   5;      5<      5=      5>      5?      5@      