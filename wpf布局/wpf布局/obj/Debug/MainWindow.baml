   M S B A M L   `   `   `  ����D  @wpf布局, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:wpf布局	wpf布局  5   	       wpf布局.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:wpf布局     5   	   -   5          � Title$  
MainWindow��5   	   $
��1000��6   $	��800��6*    � WindowStartupLocation$ CenterScreen=�6O   .��5	      � -   5         �Name$ grdPic��5	       � ColumnDefinitions 5
   
   �� 5      $��50��6   �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      �� 5      �� 5      �� 5      .+�5   
   ]� $��0��6   $��0��6!   $��4��61   
����ح�.+�5      �� -   5         ��Name$ 
btnProcess��5      $
��Left=�6)   $��开始��6?   $��50��6L   $��19��6W   $��3,0,0,0q�6c   6t   �� 5      -   5         ��Name$ 
lblPercent��5      $��Right=�6(   $
��0.0%��6?   $��60��6N   $��Right=�6Y   $��Center=�6|   � 5      -   5         �Name$ pbShow��5       � Value$ 0Q�6*   $��3,0,3,0q�64   $��19��6E   6S   5      5       5"      