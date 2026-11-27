# PerfilAlumno

Aplicación desarrollada en .NET MAUI para la materia Desarrollo de Aplicaciones Móviles II.

## Descripción

La aplicación permite gestionar un perfil de alumno, almacenar sus datos de manera persistente y consultar usuarios mediante una API REST.

## Funcionalidades

- Visualización y edición del perfil del alumno.
- Validación de nombre y edad.
- Navegación entre pantallas mediante Shell.
- Consumo de API REST JSONPlaceholder.
- Visualización del listado y detalle de usuarios.
- Persistencia de datos mediante SQLite.
- Captura de fotografías con la cámara.
- Obtención de ubicación mediante geolocalización.
- Lectura de datos del acelerómetro.
- Vibración como confirmación del guardado.
- Gestión de permisos en tiempo de ejecución.
- Manejo de excepciones y mensajes al usuario.

## Tecnologías utilizadas

- C#
- .NET 9
- .NET MAUI
- XAML
- SQLite
- CommunityToolkit.Maui
- xUnit
- Moq
- GitHub Actions

## Arquitectura y buenas prácticas

- Patrón MVVM.
- Patrón Repository.
- Inyección de dependencias.
- Separación de responsabilidades por capas.
- Centralización de validaciones en la capa de servicios.
- Separación del modelo de dominio y la entidad de persistencia.

## Refactorizaciones desde el primer parcial

- Inyección de dependencias de HttpClient.
- Implementación de persistencia mediante SQLite.
- Incorporación de interfaces y repositorios.
- Creación de un servicio para la gestión del perfil.
- Desacoplamiento del modelo de dominio respecto de SQLite.
- Centralización de las validaciones.

## Testing

El proyecto PerfilAlumno.Tests incluye:

- Pruebas unitarias con xUnit.
- Pruebas con Fake Repository.
- Pruebas de integración con SQLite In-Memory.
- Pruebas con mocks utilizando Moq.
- Pruebas de servicios y validaciones.

Para ejecutar las pruebas:

```bash
dotnet test PerfilAlumno.Tests/PerfilAlumno.Tests.csproj
```

## Integración continua (CI/CD)

El proyecto cuenta con un pipeline de GitHub Actions que realiza:

- Restauración de dependencias.
- Compilación y ejecución de pruebas.
- Compilación para Android y Windows mediante matrices.
- Publicación de la aplicación.
- Generación de artefactos.
- Caché de paquetes NuGet.

## Estado actual

La aplicación fue probada en Windows y cuenta con un pipeline de
integración continua ejecutado correctamente para Android y Windows.

Quedan pendientes las pruebas físicas de sensores en dispositivos
móviles compatibles y las pruebas de ejecución en iOS.

## Autora

Victoria Imoberdorff

Tecnicatura Universitaria en Programación de Sistemas.