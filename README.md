# Pequeños Grandes Problemas 

*Un videojuego educativo para sensibilizar sobre la salud mental a alumnos de primaria.*

## Descripción general 

**Pequeños Grandes Problemas** es un videojuego educativo desarrollado en Unity, creado como proyecto de fin de grado, con el objetivo de concienciar a estudiantes de primaria sobre la importancia de la salud mental. A través de distintos mini-juegos, el juego introduce a los niños en conceptos emocionales de forma lúdica e interactiva. La premisa central es que incluso los problemas emocionales "pequeños" en la niñez pueden sentirse como grandes problemas, y el juego busca validar esas emociones y ofrecer herramientas para manejarlas. 

Este proyecto nace de la idea de aprovechar el poder del juego para el aprendizaje socioemocional. Según UNICEF, jugar permite a los niños expresar y procesar emociones complejas de una forma segura[1]. Partiendo de esta base, Pequeños Grandes Problemas utiliza la diversión y la interactividad del videojuego para abrir conversaciones sobre el bienestar emocional, ayudar a eliminar tabúes y fomentar la empatía desde edades tempranas. 

## Objetivos del juego 

- **Sensibilización sobre salud mental:** Presentar a los niños nociones básicas sobre emociones y problemas psicológicos comunes (como el miedo, la ansiedad o la tristeza) de una manera adaptada a su edad. El juego muestra que todos enfrentamos dificultades emocionales y que es importante comunicarlas y buscar ayuda cuando sea necesario. 

- **Educación lúdica:** Integrar contenido educativo en mecánicas de juego atractivas. Los mini-juegos están diseñados para que los niños aprendan indirectamente: mientras se divierten resolviendo un puzzle o superando un nivel de plataformas, interiorizan mensajes positivos sobre gestión emocional y resiliencia. 

- **Eliminar el estigma:** Normalizar la conversación sobre problemas emocionales en el aula. Al vivir aventuras con personajes que enfrentan sus “pequeños grandes problemas”, los estudiantes pueden sentirse más cómodos hablando de sus propias emociones en la vida real. 

# Mecánicas de juego y mini-juegos 

La experiencia de Pequeños Grandes Problemas está compuesta por múltiples mini-juegos, cada uno con una mecánica diferente. Esto permite abordar la salud mental desde varios enfoques y mantener la atención de los jugadores con variedad en la jugabilidad. A continuación se describen los mini-juegos principales incluidos: 

### 😮‍💨 Minijuego de ritmo: “Nervios”

“Nervios” es el primer mini-juego del prototipo. En él, se ayuda a **PsyBot**, un pequeño robot abrumado por los nervios antes de un examen, a aprender y practicar dos técnicas de respiración fundamentales:

- **Método del cuadrilátero** (nivel iniciación): inspira, mantén, espira y vuelve a mantener durante intervalos iguales.  
- **Método 4-7-8** (nivel avanzado): inspira 4 s, mantén 7 s, espira 8 s.

#### Contexto y ambientación  
Al comenzar, el escenario de una aula se llena de una niebla oscura que refleja la ansiedad de PsyBot. Sus compañeros permanecen en las sombras, observándolo con gestos de juicio. Un personaje guía, de aspecto amable, aparece para explicar paso a paso cómo realizar la respiración según el nivel elegido.

#### Mecánica de juego  
Cada respiración consta de **tres fases**, y el jugador debe completarlas en orden **tres veces** sin errores:

1. **Inspirar**  
   - Acción: pulsa y mantén clic izquierdo (o toca el lado izquierdo de la pantalla)  
   - Duración: la que marque la técnica seleccionada  
2. **Mantener**  
   - Acción: no pulsar nada durante el tiempo indicado  
3. **Espirar**  
   - Acción: pulsa y mantén clic derecho (o toca el lado derecho de la pantalla)  
   - Duración: la que marque la técnica seleccionada  

La interfaz muestra un **aro azul** con un anillo interior luminoso que:

- Se expande hacia el borde exterior al inspirar.  
- Permanece fijo durante la fase de mantener.  
- Se contrae hacia el borde interior al espirar.  

Un **contador numérico** y mensajes en pantalla indican en qué fase estás y qué debes hacer. Además, el icono del ratón (o el color de la mitad de la pantalla) parpadea para señalar la acción a realizar.

#### Retroalimentación y puntuación  
- Cada fase completada **correctamente** otorga **50 puntos**, un efecto de partículas y un mensaje positivo.  
- Si la fase es demasiado corta o larga, se reproduce un sonido de error, aparece un mensaje de aliento y debes reiniciar esa respiración desde la fase de inspirar.  
- Tras completar las tres respiraciones correctas, la niebla se disipa, los compañeros recuperan su tono amistoso y tu puntuación final queda registrada según el número de fases completadas sin error.

---

*Esta mecánica busca enseñar sin parecer un tutorial tradicional: a través del juego, los niños interiorizan técnicas de respiración cuyos beneficios han sido validados científicamente (método del cuadrilátero y método 4-7-8) y mejorar su bienestar emocional de forma intuitiva.* 

(Nota: la música empleada es alegre y está pensada para crear un ambiente distendido. Este mini-juego también mejora la coordinación mano-ojo de los niños y su capacidad de atención, competencias que son beneficiosas tanto para el juego como para su día a día.) 

### 🕹️ Minijuego de plataformas: “Tristeza” 

En este nivel, el jugador controla a **PsyBot**, un personaje robot en un entorno 3D de plataformas. En él, se debe ayudar al protagonista a reencontrarse con sus pasatiempos favoritos.

En “Tristeza”, PsyBot atraviesa **tres niveles** con perspectiva en tercera persona. Para avanzar, el jugador debe:

- **Recolectar coleccionables:**  
  - **Monedas:** Determinan la puntuación final.  
  - **Estrellas (3 por nivel):** Recuperan el color del mundo y hacen aparecer el nuevo pasatiempo.  
  - **Hobbies:** Claves para acceder al siguiente nivel; su aparición se señala con un cambio de cámara enfocando el objetivo.

- **Superar obstáculos:** Plataformas, huecos y elementos del escenario que requieren saltos y movimientos precisos.

- **Mecánica “Pedir ayuda”:**  
  - **Nivel 1 (huecos sin salida):** El puente colapsa y atrapa al jugador, sirviendo como tutorial de la mecánica.  
  - **Niveles 2 y 3 (pensamientos negativos):** Nubes de partículas persiguen al jugador. Al colisionar, bloquean a PsyBot hasta que el jugador presiona repetidamente la tecla “E” para llenar una barra de progreso que, al completarse, transporta al personaje a una zona segura o disipa la nube.

Al completar los tres niveles, PsyBot habrá recuperado sus pasatiempos, la saturación de la cámara volverá a la normalidad y la puntuación final se definirá por las monedas recolectadas.

(Tecnología destacada: se utiliza el asset Jammo Character para el protagonista, lo que aportó un modelo 3D riggeado profesionalmente listo para animar[2]. Además, se implementó una cámara dinámica con Cinemachine para seguir al personaje en tercera persona de forma fluida[3].) 

### 🧩 Minijuego de puzzle deslizante: “Dispersión”

“Dispersión” es el último mini-juego del prototipo y adopta la mecánica de puzle deslizante con la premisa de liberar una idea que se ha quedado atrapada en la mente.

La elección del puzle deslizante responde al objetivo de **aumentar la concentración** de los jugadores. Está demostrado que los rompecabezas mejoran habilidades cognitivas como la memoria, la atención al detalle y la resolución de problemas, competencias esenciales para el público infantil del prototipo.

- **Dinámica de juego**  
  - El jugador debe apartar obstáculos desplazando las piezas de un tablero para **trazar un camino** que guíe a la idea (representada por una bombilla) hasta la salida.  
  - Cada pieza se mueve pulsando y arrastrando hacia uno de los huecos libres dentro del marco rosa.  
  - Cada desplazamiento incrementa un **contador de movimientos** mostrado en la interfaz.

- **Controles y ayudas**  
  - **Clicks/Arrastre:** Selecciona y desliza piezas en dirección de un hueco adyacente.  
  - **Botón “Reiniciar”:** Restablece las piezas a su posición inicial y pone el contador de movimientos a cero, permitiendo volver a intentarlo desde el principio.

- **Niveles de dificultad**  
  - Existen **dos niveles** de complejidad, cada uno con un diseño de puzle distinto.  
  - Al resolver el puzle, se presenta un menú final que muestra:
    1. Movimientos realizados.  
    2. Movimientos óptimos necesarios.  
  - Esta comparación reta al jugador a **optimizar su estrategia** y completar el puzle en el menor número de movimientos posible.

Con “Dispersión”, los niños entrenan su concentración y pensamiento lógico de forma lúdica, reforzando la idea de que, con paciencia y estrategia, cualquier idea atrapada puede ser liberada.

## Desarrollo y tecnología utilizada

El proyecto **Pequeños Grandes Problemas** fue desarrollado íntegramente con el motor Unity y lenguaje C#. A nivel técnico, se combinaron distintos sistemas y herramientas para dar vida a los mini-juegos y lograr una presentación pulida. A continuación, se destacan los aspectos de ingeniería y recursos utilizados:

- **Unity (C#):**  
  Se utilizó Unity como engine principal para aprovechar su versatilidad en 2D y 3D dentro de un mismo proyecto. La estructura del juego está organizada en escenas independientes para cada mini-juego (plataformas, puzzle, ritmo), lo que facilitó el diseño modular.  
  Los scripts en C# siguen el paradigma de componentes de Unity (MonoBehaviours) para controlar las mecánicas de cada escena. Por ejemplo, hay scripts para el movimiento del personaje, gestión de colisiones y físicas en el nivel de plataformas; scripts para la lógica de puzzles deslizantes; y controladores de ritmo que manejan la sincronización de las entradas del jugador con las respiraciones del nivel "Nervios". Unity también proporcionó sistemas integrados como el motor de físicas (usado en el mini-juego de plataformas) y el sistema de interfaz de usuario (UI) para menús y HUD.

- **Jammo Character (Mix and Jam asset):**  
  Para representar al protagonista del juego, se integró el modelo 3D **Jammo**, un personaje gratuito de estilo cartoon proporcionado por la comunidad de Mix and Jam. Este modelo resultó ideal por ser amigable para niños y venir ya optimizado. Jammo está riggeado y preparado para funcionar con animaciones de Mixamo, lo que aceleró la implementación de movimientos como correr, saltar o celebrar sin requerir animaciones propias desde cero. Se importaron animaciones prediseñadas (a través de Mixamo) para las acciones básicas del personaje, ajustándolas ligeramente para encajar con la estética y ritmo del juego. La cámara del nivel de plataformas se implementó con **Cinemachine**, permitiendo un seguimiento en tercera persona suave y configuraciones de cámara predefinidas sin necesidad de programar manualmente el comportamiento de la cámara. Esto aportó un acabado más profesional al mini-juego de plataformas y mejor experiencia de usuario al mantener siempre enfocado al personaje principal.

- **LeanTween:**  
  Para las transiciones de interfaz y ciertas animaciones sencillas se utilizó la librería **LeanTween** [4], un motor de tweening eficiente para Unity. LeanTween permitió crear animaciones de UI (como desvanecimientos, desplazamientos y escalados de menús) de forma muy sencilla mediante código, evitando tener que animar a mano cada elemento. Por ejemplo, al completar un nivel se muestra un panel de “Nivel Superado” que aparece con una suave transición de desplazamiento. El uso de LeanTween hizo las interfaces más dinámicas y atractivas, mejorando la experiencia del usuario con efectos pulidos sin sacrificar rendimiento.

- **Arte y diseño visual:**  
  Aunque se aprovecharon algunos assets gratuitos (como el personaje Jammo), gran parte de los escenarios y elementos visuales fueron diseñados específicamente para este proyecto. Se crearon entornos coloridos y amigables para cada mini-juego: un mundo de plataformas con estética low-poly; un tablero para el puzzle que representase una idea atrapada en la mente; y una clase estilo cartoon. La paleta de colores es suave y alegre, reforzando que aunque se hablen de problemas serios, el tono del juego es esperanzador y positivo. Asimismo, la interfaz de usuario (menús, botones, textos) utiliza íconos claros y lenguaje sencillo en español, adaptado a la comprensión de niños de primaria.

- **Audio y música:**  
  Cada mini-juego cuenta con una banda sonora adaptada a su temática y efectos de sonido que ofrecen retroalimentación inmediata. La música es libre de derechos y refuerza la ambientación emocional de cada escena.

# Vídeo demostrativo

Para apreciar mejor cómo funciona **Pequeños Grandes Problemas**, puedes ver el vídeo resumen del juego donde se muestran fragmentos de cada mini-juego y la dinámica general:  
[Ver vídeo demo en Vimeo](https://vimeo.com/1082677904)

(Si el enlace anterior no funciona, copia y pega la URL https://vimeo.com/1082677904 en tu navegador). 

# Conclusiones del proyecto

**Pequeños Grandes Problemas** demuestra la viabilidad de integrar mecánicas de juego variadas en un solo título educativo para abordar un tema sensible como la salud mental infantil. Desde el punto de vista técnico, el desarrollo implicó crear tres experiencias de juego distintas (plataformas, puzzle, pulsaciones) y unificarlas bajo una misma narrativa y estilo, lo cual puso a prueba la capacidad de gestión de escenas y modularidad del código. El resultado es un juego completo que sirve tanto a propósitos académicos (como iniciativa de gamificación en el aula para tratar emociones) como profesionales, exhibiendo habilidades de programación, diseño de niveles, uso de assets de terceros e integración de sistemas en Unity.

Este proyecto se presentó con éxito en el ámbito académico, cumpliendo con los objetivos propuestos de sensibilización. Asimismo, funciona como una demostración de competencias en desarrollo de videojuegos: manejo del motor Unity, conocimiento de herramientas como LeanTween para animación de interfaces, integración de modelos 3D y animaciones, y diseño centrado en el usuario infantil.

En suma, **Pequeños Grandes Problemas** no solo entretiene a los más pequeños, sino que también abre la puerta a conversaciones importantes sobre cómo nos sentimos. Es un ejemplo de cómo la tecnología y la creatividad pueden unirse para generar un impacto positivo en la educación emocional de los niños.

 # Citas

[1] OSIM | Obra Social de Personal de Direccíon 

https://osim.com.ar/osim_2016/pds_nota.php?n=juego_salud_mental 

[2] GitHub - mixandjam/Jammo-Character: Official repository for the Jammo character from the Mix and Jam channel! 

https://github.com/mixandjam/Jammo-Character 

[3] Unity-Technologies/Cinemachine: A suite of camera tools for Unity that gives AAA control for every camera in your project  

https://github.com/Unity-Technologies/com.unity.cinemachine

[4] GitHub - dentedpixel/LeanTween: LeanTween is an efficient animation engine for Unity 

https://github.com/dentedpixel/LeanTween 
