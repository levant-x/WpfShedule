   M S B A M L   `   `   `  ����F  BWpfSchedule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfScheduleWpfSchedule  5   	       WpfSchedule.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   +localclr-namespace:WpfSchedule     5   	   O KFluent, Version=4.0.3.394, Culture=neutral, PublicKeyToken=3e436e32a8c5546fFluenturn:fluent-ribbon  5   	   -   5          Fluent.RibbonWindow   Name$  ribbonWindow��5       � Title$ Расписание��5   	   $	��350��5   	   $	��525��6    � 	Resources 5	      ��%    5      .��5      ��  �� Orientation$ Vertical=�6   .+�5   
     Fluent.Ribbon  -   5        $��ribbon��5         Menu 5        Fluent.Backstage  5      5         Tabs. 5        Fluent.RibbonTabItem     Header$
 Home��6#      Groups. 5        Fluent.RibbonGroupBox     Header$ Group��6(   .@�5        Fluent.Button  	   Header$	 Green��6$   
   Icon$
 Images\Green.png��5         	LargeIcon$ Images\GreenLarge.png��5        5      $
	 Grey��6$   $
 Images\Gray.png��5      $ Images\GrayLarge.png��5      5      5        5       $ Группы��6#   . 5!        $ 
Курсы��6(   .@�5"        Fluent.RadioButton  -   5        $��r1��5"   )      Header. 5#   7   (1)5%        5&      -   5        $��r2��5&   )   . 5'   7   (2)5)        5*      -   5        $��r3��5*   )   . 5+   7   (3)5-        5.      -   5        $��r4��5.   )   . 5/   7   (4)51      52        53      $	��100��54      $	��500��55      .@�56      � -   5        $��panelGroups��56       58      59      5:      5;      �� 5<   
   $ 
Horizontal=�6   $��5q�5=      $��Center=�5>      .+�5?      �� -   5        $��
labelGroup��5?      $��24(�5@      $��Center=�5A      $
��Bold"�5B      
�������$��0,0,20,0q�5D      
��� ���.��5E   ,   
ПИ-2205G      �� 5H      -	   5        $��btnWeekLeft��5H      $��30(�5J      $	��◄��5K      �� 5L      $	��250��6   $��Center=�5M      
��� ���._�5O      �� -
   5        $��labelWeekState��5O      $��Center=�5P      
�������$��4q�5R      $��26(�5S      .��6"   Первая неделя5U      5V      �� 5W      -   5        $��btnWeekRight��5W      $��30(�5Y      $	��►��5Z      5[      �� 5\   
   -   5        $��myScrollViewer��5]      $��Visible=�5^      .��5_      � -   5        $��panelSchedule��5_      $��Center=�5`      5a      5b      5c      5d      