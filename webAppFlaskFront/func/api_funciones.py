import requests

#CREAMOS UNA FUNCION GENERICA QUE NOS PERMITA BUSCAR CUALQUIER REGISTRO EN CUALQUIER TABLA
def buscar_registro(api, registro, campo):
    url = "https://localhost:7259/api/" + api
    response = requests.get(url, verify=False)
    if response.status_code == 200:
        respuesta = response.json()
        for r in respuesta:
            if r[campo] == registro:
                return True, r
        return False
    else:
        return False

def buscar_registro_doble(api, registro1, registro2, campo1, campo2):
    url = "https://localhost:7259/api/" + api
    response = requests.get(url, verify=False)
    if response.status_code == 200:
        respuesta = response.json()
        for r in respuesta:
            if r[campo1] == registro1 and r[campo2] == registro2:
                return True, r
        return False
    else:
        return False

"""
ESTA ES LA SECCION QUE TENDRA LA FUNCIONES DEL USUARIO
"""

#CREAMOS LA FUNCION DE VERIRFICAR INGRESO
def verificar_ingreso(usuario, contrasena):
    url = "https://localhost:7259/api/Usuarios"
    response = requests.get(url, verify=False)
    if response.status_code == 200:
        usuarios = response.json()
        for u in usuarios:
            if u["usuario1"] == usuario and u["contrasena"] == contrasena:
                return True, u
        return False
    else:
        return False
    
#CREAMOS UNA FUNCION QUE CARGE LOS REGISTROS DE LA API
def cargar_registros(api):
    url = "https://localhost:7259/api/" + api
    response = requests.get(url, verify=False)
    if response.status_code == 200:
        registros = response.json()
        return registros
    else:
        return "no se encontraron registros"

"""
ESTA ES LA SECCION QUE TENDRA LA FUNCIONES DEL SELECCION
"""
    
#CREAMOS UNA FUNCION QUE NOS PERMITA AGREGAR INFORMACION
def agregar_registro(nombre_seleccion, corriente, tension, instalacion, material, nombre_proyecto):
    corriente_num = int(corriente)
    tension_num = int(tension)
    url = "https://localhost:7259/api/Seleccions"
    data = {
        "nombreSeleccion": nombre_seleccion,
        "corriente": corriente_num,
        "tension": tension_num,
        "instalacion": instalacion,
        "material": material,
        "nombreProyecto": nombre_proyecto
    }
    response = requests.post(url, json=data, verify=False)
    if response.status_code == 201:
        nuevo_registro = response.json()
        print("Se agregó el registro con el ID", nuevo_registro["id"])
        return True, nuevo_registro
    else:
        print("Error al agregar el registro:", response.text)
        return False
    
"""
ESTA ES LA SECCION QUE TENDRA LA FUNCIONES DEL CATALOGO Y EL GUARDADO DE SELECCION
"""

#CREAMOS LA FUNCION DE AMPACIDAD
def ampacidad(corriente):
    if(corriente < 40):
        return 1.26
    elif(corriente >= 40):
        return 2.5
    else:
        return 0

#CREAMOS LA FUNCION DE VERIFICAR PERMISOS
def verificar_permisos(usuario):
    proyecto = cargar_registros("Proyectoes")
    validacion = buscar_registro("Proyectoes", proyecto["usuario"], usuario["usuario1"])

    if(validacion == True):
        return True;
    else:
        return False;