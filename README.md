# Veterinary

Los endpoints estan separados por:

- Los gets de cada tabla y su versionado.
- Los gets de las consultas y su versionado.

Nota: Tener en cuenta que la autenticacion para poder usar los endpoints es por medio del bearer token.

Para el uso de los endpoints en la version 1.1 se debe establecer el Header y los Query parameters de la siguiente forma:

#### Headers

![Header](https://github.com/jeangalvis/Veterinary/assets/137228150/4edb2104-3840-4438-ba04-a3549143bd0e)

#### Query Parameters

Algunos endpoints permiten recibir un parametro mas "name" para busquedas especificas de un nombre, asi como tambien otros no hacen uso del parametro Search.

![Queries](https://github.com/jeangalvis/Veterinary/assets/137228150/16464107-359c-4e84-bd8f-665866c0abe1)

## Endpoints Usuarios

Para los endpoints de Register y AddRole es necesario estar autenticado con un usuario que tenga rol de "Administrator"

##### 1. Register

`http://localhost:5036/api/User/register`

Body en formato JSON

```JSON
{
  "email": "string",
  "username": "string",
  "password": "string"
}
```

##### 2. Login

Aca obtenemos el token para usarlo como autenticación en el Bearer Token de las consultas

`http://localhost:5036/api/User/token`

Body en formato JSON

```JSON
{
  "username": "string",
  "password": "string"
}
```

##### 2. AddRole

`http://localhost:5036/api/User/addrole`

Body en formato JSON

```JSON
{
  "username": "string",
  "password": "string",
  "role": "string"
}
```

## Endpoints Get tablas

##### 1. Appointment

Lista de citas para las mascotas

`http://localhost:5036/api/Appointment` Version 1.0

`http://localhost:5036/api/Appointment?PageIndex=1&PageSize=5` Version 1.1

##### 2. Breed

Lista de razas de las mascotas

`http://localhost:5036/api/Breed` Version 1.0

`http://localhost:5036/api/Breed?PageIndex=1&PageSize=5` Version 1.1

##### 3. MedicalTreatment

Lista de tratamientos medicos

`http://localhost:5036/api/MedicalTreatment` Version 1.0

`http://localhost:5036/api/MedicalTreatment?PageIndex=1&PageSize=5` Version 1.1

##### 4. Medicine

Lista de medicamentos en la veterinaria

`http://localhost:5036/api/Medicine` Version 1.0

`http://localhost:5036/api/Medicine?PageIndex=1&PageSize=5` Version 1.1

##### 5. Owner

Lista de Propietarios de mascotas

`http://localhost:5036/api/Owner` Version 1.0

`http://localhost:5036/api/Owner?PageIndex=1&PageSize=5` Version 1.1

##### 6. Pet

Lista de mascotas registradas en la veterinaria

`http://localhost:5036/api/Pet` Version 1.0

`http://localhost:5036/api/Pet?PageIndex=1&PageSize=5` Version 1.1

##### 7. PurchasedMedicine

Lista de medicamentos comprados

`http://localhost:5036/api/PurchasedMedicine` Version 1.0

`http://localhost:5036/api/PurchasedMedicine?PageIndex=1&PageSize=5` Version 1.1

##### 8. SoldMedicine

Lista de medicamentos vendidos

`http://localhost:5036/api/SoldMedicine` Version 1.0

`http://localhost:5036/api/SoldMedicine?PageIndex=1&PageSize=5` Version 1.1

##### 9. Species

Lista de especies de mascotas

`http://localhost:5036/api/Species` Version 1.0

`http://localhost:5036/api/Species?PageIndex=1&PageSize=5` Version 1.1

##### 10. Supplier

Lista de proveedores de medicamentos

`http://localhost:5036/api/Supplier` Version 1.0

`http://localhost:5036/api/Supplier?PageIndex=1&PageSize=5` Version 1.1

##### 11. Veterinarian

Lista de veterinarios

`http://localhost:5036/api/Veterinarian` Version 1.0

`http://localhost:5036/api/Veterinarian?PageIndex=1&PageSize=5` Version 1.1

## Endpoints Consultas

##### 1. Crear un consulta que permita visualizar los veterinarios cuya especialidad sea Cirujano vascular.

`http://localhost:5036/api/Veterinarian/GetVeterinarianxSpeaciality` version 1.0

`http://localhost:5036/api/Veterinarian/GetVeterinarianxSpeaciality?PageIndex=1&PageSize=5` Version 1.1

##### 2. Listar los medicamentos que pertenezcan a el laboratorio Genfar

`http://localhost:5036/api/Medicine/GetMedicinesxSupplier` Version 1.0

`http://localhost:5036/api/Medicine/GetMedicinesxSupplier?PageIndex=1&PageSize=5` Version 1.1

##### 3. Mostrar las mascotas que se encuentren registradas cuya especie sea felina.

`http://localhost:5036/api/Pet/GetPetsxSpecie` Version 1.0

`http://localhost:5036/api/Pet/GetPetsxSpecie?PageIndex=1&PageSize=5` Version 1.1

##### 4. Listar los propietarios y sus mascotas.

`http://localhost:5036/api/Owner/GetOwnersWithPets` Version 1.0

`http://localhost:5036/api/Owner/GetOwnersWithPets?PageIndex=1&PageSize=5` Version 1.1

##### 5. Listar los medicamentos que tenga un precio de venta mayor a 50000.

`http://localhost:5036/api/Medicine/GetMedicinesMoreExpensiveThan` 1.0

`http://localhost:5036/api/Medicine/GetMedicinesMoreExpensiveThan?PageIndex=1&PageSize=5` Version 1.1

##### 6. Listar las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023.

`http://localhost:5036/api/Pet/GetPetsxReason` Version 1.0

`http://localhost:5036/api/Pet/GetPetsxReason?PageIndex=1&PageSize=5` Version 1.1

##### 7. Listar todas las mascotas agrupadas por especie.

`http://localhost:5036/api/Pet/GetPetsGroupBySpecie` Version 1.0

`http://localhost:5036/api/Pet/GetPetsGroupBySpecie?PageIndex=1&PageSize=5` Version 1.1

##### 8. Listar todos los movimientos de medicamentos y el valor total de cada movimiento.

`http://localhost:5036/api/SoldMedicine/GetMovMedWithTotal` Version 1.0

`http://localhost:5036/api/SoldMedicine/GetMovMedWithTotal?PageIndex=1&PageSize=5` Version 1.1

##### 9. Listar las mascotas que fueron atendidas por un determinado veterinario.

Esta consulta recibe el nombre del veterinario por url
`http://localhost:5036/api/Pet/GetPetsxVeterinarian/{name}` Version 1.0
Esta consulta recibe el nombre del veterinario por parametro
`http://localhost:5036/api/Pet/GetPetsxVeterinarian?PageIndex=1&PageSize=5` Version 1.1

##### 10. Listar los proveedores que me venden un determinado medicamento.

Esta consulta recibe el nombre del medicamento por url
`http://localhost:5036/api/Supplier/GetSupplierxMedicine/{name}` Version 1.0
Esta consulta recibe el nombre del medicamento por parametro
`http://localhost:5036/api/Supplier/GetSupplierxMedicine?PageIndex=1&PageSize=5` Version 1.1

##### 11. Listar las mascotas y sus propietarios cuya raza sea Golden Retriver.

`http://localhost:5036/api/Pet/GetPetsGoldenRetriever` Version 1.0

`http://localhost:5036/api/Pet/GetPetsGoldenRetriever?PageIndex=1&PageSize=5` Version 1.1

##### 12. Listar la cantidad de mascotas que pertenecen a una raza.

`http://localhost:5036/api/Pet/GetPetCountByBreed` Version 1.0

`http://localhost:5036/api/Pet/GetPetCountByBreed?PageIndex=1&PageSize=5` Version 1.1

###End
