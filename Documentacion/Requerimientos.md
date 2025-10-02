Requerimientos del Proyecto RCS-TecMed
🧭 Objetivo General
Diseñar e implementar un sistema informático que reorganice digitalmente los procesos administrativos, documentales y de atención del Colegio de Tecnólogos Médicos de Chile, asegurando accesibilidad, escalabilidad y claridad para todos los usuarios.

🧩 Requerimientos Funcionales
1. Gestión de miembros
Registro, edición y eliminación de tecnólogos médicos.
Visualización de historial de membresía y pagos.
Validación de campos obligatorios (RUT, correo, nombre completo).
2. Administración de cuotas y pagos
Registro de cuotas por periodo.
Asociación de pagos a miembros.
Exportación de reportes en formato delimitado por pipe (|).
3. Generación de documentos
Emisión de certificados de membresía.
Generación de informes para el directorio.
Exportación de datos visibles desde DataGrid.
4. Validaciones y retroalimentación
Validación de correos electrónicos con mensajes amigables.
Validación de RUT con retroalimentación clara.
Mensajes de error adaptados para usuarios no técnicos.
🧪 Requerimientos No Funcionales
1. Usabilidad
Interfaz accesible para usuarios neurodivergentes y no técnicos.
Lenguaje claro y visuales adaptados para presentaciones institucionales.
2. Rendimiento
Respuesta rápida en operaciones CRUD.
Exportaciones optimizadas para grandes volúmenes de datos.
3. Seguridad
Control de acceso por roles (administrador, revisor, invitado).
Validación de entrada para prevenir inyecciones SQL.
4. Escalabilidad
Base de datos preparada para migración futura a Azure.
Modularidad para incorporar plataforma web en fases posteriores.
📌 Consideraciones Especiales
Toda documentación debe ser editable y presentable ante el directorio.
Se utilizarán herramientas como Umbrella y SSMS Diagrammer para modelado.
Se respetarán convenciones internas de codificación, especialmente en nombres de variables y estructuras de validación.
📅 Entregables por Fase
Fase	Entregable	Fecha estimada
1	Base de datos y módulo escritorio inicial	Octubre 2025
2	Validaciones y exportaciones	Noviembre 2025
3	Documentación y presentación institucional	Diciembre 2025
