from flask import Flask, render_template, request, redirect, url_for
from func import api_funciones
import re

app = Flask(__name__)

# ruta para la página principal
@app.route('/')
def index():
    return redirect(url_for('webapp'))

@app.route('/webapp', methods=['POST', 'GET'])
def webapp():
    #CREAMOS UNA VARIABLE ALERTA QUE VA A TENER LO MENSAJES QUE VAMOS A MOSTRAR
    alerta = ""
    alerta2 = ""
    usuario = []
    registros_usuario = []
    usuario_en_proyecto = []
    ampacidad = 0.0

    if request.method == 'POST':
        nombre = request.form.get('nombre', '').strip()
        contrasena = request.form.get('contrasena', '').strip()

        if (nombre and contrasena):
            #CREAMOS LA VARIABLE USUARIO
            usuario = api_funciones.verificar_ingreso(nombre, contrasena)
            if usuario[0]:
                alerta = "El usuario esta dentro de la base de datos"
                return redirect(url_for('webapp_seleccion'))

            else:
                alerta = "No parece que exista este usuario en la base de datos"
        else:
            alerta = "Alguno de los campos está vacío"

    return render_template(
        "web_app.html", 
        alerta=alerta,
        alerta2=alerta2,
        usuario=usuario, 
        registros_usuario=registros_usuario,
        usuario_en_proyecto=usuario_en_proyecto,
        ampacidad = ampacidad
    )

@app.route('/webapp_seleccion', methods=['POST', 'GET'])
def webapp_seleccion():
    alerta = ""
    ampacidad = 0.0
    #MOSTRAMOS LOS REGISTROS DE LOS USUARIOS
    registros_usuarios = api_funciones.cargar_registros("Usuarios")
    registros_proyectos = api_funciones.cargar_registros("Proyectoes")
    registros_catalogo = api_funciones.cargar_registros("Catalogoes")
    seleccion = []
    seleccion_catalogo = []

    #REVISAMOS AL INFORMACION DEL FORMULARIO
    if request.method == 'POST':
        nombre_seleccion = request.form['nombre_seleccion']
        corriente = request.form['corriente']
        tension = request.form['tension']
        instalacion = request.form['instalacion']
        material = request.form['material']
        nombre_proyecto = request.form['nombre_proyecto']
        if(len(corriente) and len(tension) and len(instalacion) and len(material) and len(nombre_proyecto) and len(nombre_seleccion) != 0):
            #SE AGREGAN TODOS LOS REGISTROS
            seleccion = api_funciones.agregar_registro(nombre_seleccion, corriente, tension, instalacion, material, nombre_proyecto)

            if(float(corriente) >= 40):
                ampacidad = 2.5
            elif(float(corriente) < 40):
                ampacidad = 1.26

            seleccion_catalogo = api_funciones.buscar_registro_doble("Catalogoes", "ampacidad", "material", ampacidad, material)
                
    else:
        print("")

    return render_template(
        "web_app_seleccion.html",
        registros_usuarios = registros_usuarios,
        alerta = alerta,
        ampacidad = ampacidad,
        registros_proyectos = registros_proyectos,
        registros_catalogo = registros_catalogo,
        seleccion_catalogo = seleccion_catalogo,
        seleccion = seleccion
    )

@app.route('/webapp_catalogo', methods=['POST', 'GET'])
def webapp_catalogo():
    return "hola"

if __name__ == '__main__':
    app.run(debug=True)
