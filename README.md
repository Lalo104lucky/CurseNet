# CsBases

Proyecto de práctica para repasar bases de C# y .NET con ejemplos organizados por temas.

## Objetivo

Este repositorio reúne ejercicios pequeños para entender y probar conceptos fundamentales del lenguaje y del runtime, con código sencillo y enfocado en la consola.

## Temas trabajados

- Tipos básicos y declaraciones con `var`.
- Herencia e interfaces con `IProduct`, `Product` y `ServiceProduct`.
- Sobrescritura de métodos con `GetDescription()`.
- Patrón adaptador con `ProductAdapter` y `ProductDto`.
- Inyección de dependencias con `ILabelService`, `LabelService` y `ProductManager`.
- Programación asíncrona con `ProductRepository` y `Task`.
- Atributos personalizados con `UpperCaseAttribute` y procesamiento por reflexión.

## Estructura principal

- `Program.cs`: punto de entrada donde se prueban los ejemplos.
- `Fundamentals/02-Tipos-Basicos`: ejemplos de tipos primitivos y sintaxis base.
- `Fundamentals/04-Herencia`: clases e interfaces para herencia.
- `Fundamentals/05-Patron-Adaptador`: transformación de objetos entre modelos.
- `Fundamentals/06-Inyeccion-Dependencias`: separación entre servicio y consumidor.
- `Fundamentals/07-Metodos-Asincronos`: ejemplo de una operación asincrónica simulada.
- `Fundamentals/08-Atributos`: atributo personalizado y aplicación de reglas con reflexión.

## Ejecución

El proyecto está configurado para `.NET 10.0`.

```bash
dotnet run
```

## Notas

- En `Program.cs` varios ejemplos están comentados para activar solo las pruebas necesarias.
- El proyecto está pensado como base de aprendizaje, así que los archivos pueden ir creciendo conforme se agreguen más temas.