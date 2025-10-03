# Cumplimiento Ley 21.719 - Protección de Datos Personales y Ciberseguridad  
**Sistema:** RCS-TecMed (Registro y Control de Socios Tecnólogos Médicos)  
**Ubicación:** /documentos/ley_proteccion_datos.md  
**Última actualización:** 02-10-2025  
**Responsables:** Pedro Arrate y Colegio de Tecnólogos Médicos de Chile

---

## 🧩 Contexto Legal

La Ley 21.719 modifica la Ley 19.628 y establece nuevas obligaciones para el tratamiento de datos personales en Chile. Entrará en vigencia el 1 de diciembre de 2026. Aplica a sistemas que recojan, almacenen o procesen datos de personas naturales ubicadas en Chile.

---

## 🔐 Principios Clave

- **Consentimiento informado**: Requerido para todo tratamiento de datos personales, salvo excepciones legales.  
- **Minimización de datos**: Solo recolectar lo estrictamente necesario.  
- **Seguridad por diseño**: Cifrado, control de acceso, trazabilidad.  
- **Derechos ARCO**: Acceso, Rectificación, Cancelación, Oposición.  
- **Portabilidad y revocación**: El titular puede solicitar sus datos o revocar el consentimiento.

---

## 🧱 Aplicación en Arquitectura MVC

### Modelo (Model)
- Clasificación de campos como personales/sensibles.  
- Cifrado de datos sensibles (AES, SHA256).  
- Metadata para trazabilidad y auditoría.

### Vista (View)
- Formularios con consentimiento explícito.  
- Interfaces para ejercer derechos ARCO.  
- Mensajes claros sobre uso de datos.

### Controlador (Controller)
- Validación de base legal para tratamiento.  
- Registro de accesos y modificaciones.  
- Gestión de solicitudes de derechos.

---

## 📋 Checklist de Cumplimiento Técnico

- [x] Política de privacidad accesible desde la interfaz.  
- [x] Registro de consentimiento por usuario.  
- [x] Logs de tratamiento y acceso.  
- [x] Evaluación de impacto (DPIA) para módulos sensibles.  
- [x] Cifrado en tránsito y reposo.  
- [x] Control de acceso por rol.  
- [x] Mecanismo de portabilidad de datos.  
- [x] Función para revocación de consentimiento.

---

## 🏛️ Referencias Normativas

- Ley 21.719 sobre Protección de Datos Personales  
- Ley 19.628 sobre Protección de la Vida Privada  
- Agencia de Protección de Datos Personales (Chile)

---

## 📌 Notas

Este documento debe actualizarse conforme avance la implementación y se publiquen reglamentos específicos. Se recomienda revisión mensual y validación jurídica antes del despliegue en producción.
