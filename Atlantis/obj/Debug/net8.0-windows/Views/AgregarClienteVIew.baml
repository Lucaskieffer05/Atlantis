   M S B A M L   `   `   `  ����C  ?Atlantis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Atlantis.RecursosAtlantis.Recursos  5   	   9!clr-namespace:Atlantis.ViewModelsAtlantis.ViewModels  5   	   '    !Atlantis.Views.AgregarClienteVIew   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Atlantis.Recursos     5   	   8
viewmodels!clr-namespace:Atlantis.ViewModels     5   	     � Title$  AgregarClienteVIew��5	   	   $	��450��6$   $	��800��61    � 	Resources 5      9  3Atlantis.Recursos.CheckBoxValueToParameterConverter   Convertidor��%   &	          5   
   62   5      ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ White��6    � Offset$ 1Q�6)   � 5      $ 	#FFBDF0F1��6   $ 0Q�6-   � 5      $ White��6   $ 0.177Q�6)   5      .��5      �  � RowDefinitions 5   
   �� 5      $	��25* �6   �� 5      $	��160 �6   �� 5      $	��50* �6   �� 5      $	��20* �6   .+�5   
   �� $��Center=�6   $��Agregar Cliente��5      $
��Wrap=�5       $��Center=�5!      $��36(�5"      $
��Bold"�5#      $��Italic$�5$      $��Cascadia Mono ExtraLight)�5%      $��0��5&      �� 5(   
   $��1��5)       �� Orientation$ Vertical=�5*      $	��250��5+      .+�5,      �� $
��Wrap=�6   $��Nombre y Apellido:��6,   $!��Bahnschrift Light Condensed)�6F   $��16(�6o   ���	#FF8686866}   �� 5-       �� TextWrapping$
 Wrap=�6J   $
��auto��6^   ��6   �� *Pago.Cliente.NomApell+	 �� Mode$	 TwoWay=�6^   �� 5/      $y�Visible=�6   $��10(�6p   
���  ��$��21��6�   ��6)   �� *ErrorNomApell+
 �� UpdateSourceTrigger$
 PropertyChanged=�6�   �� 51      $
��Wrap=�6   $
��DNI:��6,   $!��Bahnschrift Light Condensed)�68   $��16(�6a   ���	#FF8686866o   �� 52      $
 Wrap=�6E   $
��auto��6Y   ��6   �� *Pago.Cliente.dni+$	 TwoWay=�6Y   �� 54      $y�Visible=�6   $��10(�6k   
���  ��$��21��6�   ��6)   �� *
ErrorDNI+$
 PropertyChanged=�6�   �� 56      $
��Wrap=�6   $��Fecha de Nacimiento:��6,   $!��Bahnschrift Light Condensed)�6H   $��16(�6q   ���	#FF8686866   �� 57      $
 Wrap=�6Q   $
��auto��6e   ��6   �� *Pago.Cliente.fechaNacimiento+$	 TwoWay=�6e   58      � 5:   
   $��2��6   $��Center=�6    � ColumnDefinitions 5;      �� 5<      $	��100 �6#   �� 5=      $	��100 �6#   .+�5A      �� $��0��6   $ Vertical=�5B      $��Center=�5C      $��Center=�5D      .+�5E      �� -   5         ��Name$ 
checkEnero��5E   d   $��Enero��6x   $��0,0,0,8q�6�   $��15��6�   $��15(�6�    d� 	IsChecked 6   �� *
Pago.Enero+ �� 	Converter# [  6�   �� 5F      -   5        $ checkFebrero��5F   f   $��Febrero��6|   $��0,0,0,8q�6�   $��15��6�   $��15(�6�    6   �� *Pago.Febrero+# [  6�   �� 5G      -   5        $ 
checkMarzo��5G   d   $��Marzo��6x   $��0,0,0,8q�6�   $��15��6�   $��15(�6�    6   �� *
Pago.Marzo+# [  6�   �� 5H      -   5        $ 
checkAbril��5H   d   $��Abril��6x   $��0,0,0,8q�6�   $��15��6�   $��15(�6�    6   �� *
Pago.Abril+# [  6�   �� 5I      -   5        $ 	checkMayo��5I   c   $
��Mayo��6v   $��0,0,0,8q�6�   $��15��6�   $��15(�6�    6   �� *	Pago.Mayo+# [  6�   �� 5J      -   5        $ 
checkJunio��5J   d   $��Junio��6x   $��0,0,0,0q�6�   $��15��6�   $��15(�6�    6   �� *
Pago.Junio+# [  6�   5K      �� 5M      $��1��6   $ Vertical=�5N      $��Center=�5O      $��Center=�5P      .+�5Q      �� -   5        $ 
checkJulio��5Q   d   $��Julio��6x   $��0,0,0,8q�6�   $��15��6�   $��15(�6�    6   �� *
Pago.Julio+# [  6�   �� 5R      -   5        $ checkAgosto��5R   e   $��Agosto��6z   $��0,0,0,8q�6�   $��15��6�   $��15(�6�    6   �� *Pago.Agosto+# [  6�   �� 5S      -	   5        $ checkSeptiembre��5S   i   $��
Septiembre��6�   $��0,0,0,8q�6�   $��15��6�   $��15(�6�    6   �� *Pago.Septiembre+# [  6�   �� 5T      -
   5        $ checkOctubre��5T   f   $��Octubre��6|   $��0,0,0,8q�6�   $��15��6�   $��15(�6�    6   �� *Pago.Octubre+# [  6�   �� 5U      -   5        $ checkNoviembre��5U   h   $��	Noviembre��6�   $��0,0,0,8q�6�   $��15��6�   $��15(�6�    6   �� *Pago.Noviembre+# [  6�   �� 5V      -   5        $ checkDiciembre��5V   h   $��	Diciembre��6�   $��0,0,0,0q�6�   $��15��6�   $��15(�6�    6   �� *Pago.Diciembre+# [  6�   5W      5Y      �� 5[   
   $��3��6   $ 
Horizontal=�5\      $��Right=�5]      $��Center=�5^      .+�5_      �� $��Agregar��5`      $��	10,5,10,5q�6#   $��12(�67   $��33��6E   $��76��6Q   ���	#FFD6FFAF��5_      ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" � �� Path$' !DataContext.AgregarClienteCommand�5`   \   �� 5a      -   5        $��Cerrar��5a      $��	10,5,10,5q�6&   $��12(�6:   $��	25,0,25,0q�6H   $��67��6q   ���	#FFFFD3D36|   5c      5e      5f      