   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35fGclr-namespace:System.Windows.Annotations;assembly=PresentationFrameworkSystem.Windows.Annotations  5   	   M IWpfControlsAndAPIs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfControlsAndAPIsWpfControlsAndAPIs 5   	   #   WpfControlsAndAPIs.MainWindow   5   	   MaGclr-namespace:System.Windows.Annotations;assembly=PresentationFramework  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5   	   @d2http://schemas.microsoft.com/expression/blend/2008      5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5   	   2local clr-namespace:WpfControlsAndAPIs      5   	     � Title$  
MainWindow��5
   	   $	��550��6   $	��925��6)    � WindowStartupLocation$ CenterScreen=�65   .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5   
   �� -   5         ��Name$ myTabSystem��5      $��19,41,35,41q�6*   .@�5      �� -   5         ��Name$ tabInk��5      $��Ink API��6&   .��5      �� ���	#FFE5E5E5.+�5      c� -   5         c�Name$ toolBar��5      $��60��6/   .@�5      �� $��0,2.5q�6!   $	��300��60   ._�5      � .+�5   "   	� -   5         	�Name$ inkRadio��5   .   $��	Ink Mode!��6@    	� 	GroupName$ InkMode��6T   $��	0,0,200,0q�6h   6�   	� 5   "   -   5        $ selectRadio��5   .   $��Erase Mode!��6C   $ InkMode��6Y   $��100,0q�6m   6�   	� 5   "   -   5        $ 
eraseRadio��5   .   $��Select Mode!��6B   $ InkMode��6Y   $��	200,0,0,0q�6m   6�   5      5      �� 5      �� 5      -   5         ��Name$ comboColors��5   #   $	��200��68   .@�5      �� 	 �� Orientation$	 
Horizontal=�6)   

 � Tag$	
 Red��6B   .+�5   "   A� 
���  ��$��50��65   $��50��6A   �� 5   "   $��20(�6(   $��Center=�66   $��Center=�5   (   $	��Red��6C   5      �� 5      $	 
Horizontal=�6)   $
 Green��6B   .+�5   "   A� 
��� � �$��50��67   $��50��6C   �� 5    "   $��20(�6(   $��Center=�66   $��Center=�5!   (   $��Green��6C   5"      �� 5$      $	 
Horizontal=�6)   $

 Blue��6B   .+�5%   "   A� 
����  �$��50��66   $��50��6B   �� 5&   "   $��20(�6(   $��Center=�66   $��Center=�5'   (   $
��Blue��6C   5(      5)      �� 5*      -	   5         ��Name$ btnSave��5*   !   $��	Save Data��62   $��Stretch=�6F   $��Stretch=�6d   $��75��6�   6�   �� 5+      -
   5        $ btnLoad��5+   !   $��	Load Data��62   $��Stretch=�6F   $��Stretch=�6d   $��75��6�   6�   �� 5,      -   5        $ btnClear��5,   !   $��Clear��63   $��Stretch=�6C   $��Stretch=�6a   $��75��6}   6�   5-      �� 5.      -   5         ��Name$ myInkCanvas��5.       $	��140��65   $��6,0,0,0q�6B    �� 
Background
 ��ح�6S   5/      50      �� 51      -   5        $ tabDocuments��51      $��	Documents��6,   $��Bottom=�6?   $��20��6Z   .��52      �� .+�53      c� .@�54      �� 
��� � �$��Add Sticky Note��65   $5��/a:AnnotationService.CreateTextStickyNoteCommand��55   !   �� 56      
��� � �$��Delete Sticky Notes��65   $2��,a:AnnotationService.DeleteStickyNotesCommand��57   !   �� 58      
��� � �$��Highlight Text��65   $0��*a:AnnotationService.CreateHighlightCommand��59   !   5:      -� 5;      -   5         -�Name$ myDocumentReader��5;   )   $��375.4��6C   .��5<      /� .T�5=      �� 
��� ���
���   �.�5>   "   J� $��20(�6,   .*�6:   53Here are some fun facts about the WPF Document API!5@   #   5B      �� 5C      -   5         ��Name$ listOfFunFacts��5C   #   J� 5D      -   5         J�Name$ paraBodyText��5D   (   5E      5F      5H      5I      �� 5J      -   5        $ tabDataBinding��5J      $��Data Binding��6.   .��5K      � ���	#FFE5E5E56   5L      �� 5M      -   5        $ tabDataGrid��5M      $��DataGrid��6+   .��5N      � ���	#FFE5E5E56   5O      5P      5Q      5R      