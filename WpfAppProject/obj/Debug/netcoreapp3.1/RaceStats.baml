   M S B A M L   `   `   `  ����H  DWpfAppProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfAppProjectWpfAppProject  5   	   E AController, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:Controller;assembly=Controller
Controller 5   	       WpfAppProject.RaceStats   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WpfAppProject     5   	   C
Controller,clr-namespace:Controller;assembly=Controller     5   	     � Title$  RaceStatsWPF��5	   	   $	��450��6   $	��800��6+    � DataContext 5
        Controller.DataContext  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      .+�5   
   �� $��Center=�6;   $��Center=�6X   $��Center=�6s   $��Center=�6�   ��6   �� *	TotalTime+ �� Mode$ OneWay=�6�   �� 5   
   -   5         ��Name$
 Laps��5      $��1��6!   ��61   �� *Rounds+ �� View 5      �� 5      .P�5      �� $
��Name��6%    �� Width$	 100��64    �� DisplayMemberBinding 6@   �� *
Key.Name+6l   �� 5      $��	TeamColor��6%   $	 100��68    6F   �� *Key.TeamColor+6w   �� 5      $
��Laps��6%    65   �� *Value+6^   5      5      �� 5   
   -   5        $ 
Timebroken��5      $��1��6'   $��2��6\   $��	266,0,0,0q�6p   ��67   �� *SectionTimes+ 5      �� 5      .P�5       �� $��Section times��6%   $
 Auto��6=    6J   �� *Value+6s   5!      5#      �� 5$   
   $��Total time race:��6   $��Center=�6+   $��	0,162,0,0q�6H   $	��Top=�6[   5&      5'      