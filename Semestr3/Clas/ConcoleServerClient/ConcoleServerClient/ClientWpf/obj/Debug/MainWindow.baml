   M S B A M L   `   `   `  ����D  @ClientWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClientWpf	ClientWpf  5   	       ClientWpf.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ClientWpf     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� 	Response:6#   �� 5      -   5         ��Name$ 
textBlock1��5   '   $��10, 0q�6   6=   5      �� 5   
   $��1��6   $ 
Horizontal=�6"   .+�5      �� -   5        $��10q�5      
 � Tag$ _GetTime��6"   .��6F   	GetTime6O   �� 5      -   5        $��10q�5      $ _GetDate��6"   .��6F   	GetData6O   �� 5      -   5        $��10q�5      $  ��6"   .��6>   Button6F   5      5      5      