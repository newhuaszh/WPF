   M S B A M L   `   `   `  ����D  @wpf命令, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:wpf命令	wpf命令  5   	       wpf命令.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:wpf命令     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$ ApplicationCommands.Open��5
      5        5   
   -   5        $ ApplicationCommands.Save��5      5      .��5      ]� .+�5   
   o� $	��Top=�6   $
��Left=�5      
�������
���   �.@�5      m� $��_File��6   .@�5      m�  m� Command$ ApplicationCommands.Open��6   m� 5      $ ApplicationCommands.Save��6   �� 5      m� 5      $��_Exit��6   5      m� 5      $��_Edit��6   .@�5      m� $ ApplicationCommands.Copy��6   m� 5      $ ApplicationCommands.Cut��6   m� 5      $ ApplicationCommands.Paste��6   5       m� 5!      $��_Tools��6   .@�5"      m� $��_Spelling Hints��6   5#      5$      �� 5%   
   -   5         ��Name$ txtData��5%      $��Bottom=�6#   " �� VerticalScrollBarVisibility$
 Auto=�6;   $	��122��6^    �� TextWrapping$
 Wrap=�6k   $	��Top=�6   �� 5&   
   .@�5'      �� $	��123��6   .��5(      �� .+�5)      �� $	��one��6   �� 5*      $	��two��6   �� 5+      $��Three��6   5,      5-      �� 5.      $	��321��6   .��5/      �  � ColumnDefinitions 50      �� 51      �� 52      �� 53      .+�55      �� $��X��6   $��0��6)   $��0��66   �� 56      $��Y��6   $��0��6(   $��1��65   �� 57      $��Z��6   $��0��6'   $��2��64   58      59      5:      5;      5<      