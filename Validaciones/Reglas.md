✅ Reglas de Validación – Proyecto RCS-TecMed
🎯 Propósito
Este documento describe las reglas de validación aplicadas en el sistema RCS-TecMed, con el objetivo de garantizar la integridad de los datos, la claridad para el usuario y la consistencia en todos los módulos.

📌 Convenciones generales
Todas las validaciones deben:
Ser reutilizables y centralizadas cuando sea posible.
Proporcionar mensajes claros y amigables para el usuario.
Respetar las convenciones internas de nombres y estructura.
Ser compatibles con usuarios neurodivergentes y no técnicos.
🧪 Validaciones por campo
1. RUT (Rol Único Tributario)
Formato esperado: XXXXXXXX-X
Reglas:
Debe contener 7 u 8 dígitos + guion + dígito verificador.
Validación del dígito verificador mediante algoritmo Módulo 11.
Mensaje de error:
"El RUT ingresado no es válido. Verifica el formato y el dígito verificador."
2. Correo electrónico
Formato esperado: usuario@dominio.cl
Reglas:
Debe contener un solo @.
Dominio debe incluir al menos un punto (.).
No se permiten espacios ni caracteres especiales fuera de lo estándar.
Mensaje de error:
"El correo ingresado no tiene un formato válido. Ejemplo: nombre@dominio.cl"
3. Nombre completo
Reglas:
Debe contener al menos dos palabras.
No se permiten números ni símbolos.
Mensaje de error:
"Por favor ingresa el nombre completo, sin números ni símbolos."
4. Fecha de ingreso
Reglas:
No puede ser futura.
Debe tener formato dd-mm-yyyy.
Mensaje de error:
"La fecha ingresada no es válida. Verifica que no sea futura y tenga el formato correcto."
🔁 Validaciones cruzadas
Miembro activo + cuota pendiente:

Si el miembro está activo, debe tener al menos una cuota registrada en el último año.
Mensaje: "Este miembro no tiene cuotas registradas en el último periodo. Verifica su estado financiero."
Exportación de datos:

Solo se exportan columnas visibles en el DataGrid.
Separador: | (pipe)
Escapado de caracteres especiales para mantener integridad.
🧩 Validaciones técnicas
SQL Server:

Uso de CHECK, NOT NULL, DEFAULT en campos críticos.
Validaciones adicionales en procedimientos almacenados.
WPF / C#:

Validaciones en IDataErrorInfo o INotifyDataErrorInfo.
Mensajes adaptados para mostrar en ToolTip o Label.
📄 Documentación relacionada
Ver BaseDatos/ScriptsSQL/README.md para validaciones en SQL.
Ver Documentacion/Requerimientos.md para alineación funcional.
Este documento se actualiza conforme se incorporan nuevos módulos o reglas de negocio.
