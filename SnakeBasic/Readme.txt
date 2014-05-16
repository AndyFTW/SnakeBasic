============= Informationen ================

Autor: Andreas Korb
Beginn: 21.10.2013
Ende: ???

============================================
========== Vektorendefinition ==============

----------> x
|
|
|
|
V

y

Y zeigt nach unten, da es so leichter ist, Zeile für Zeile zu zeichnen.

Maximale Zeichen des Standard-Konsolenfensters:

Spalte: 35
Zeile: 25
Insgesamt: 875

============================================
================== Ablauf ==================

1. Initialisierung von 'objects' + 'comparisonObjects' mit dem Wert 'Object.Empty', da 'Empty' der Standardwert ist.
2. InitializeComponent()
    - Konsolenfenster zurücksetzen + Größe setzen
    - Timer starten + abonnieren
	- Entities erstellen (Wände, Schlange, Süßigkeit)
	- KeyDown Thread erstellen und starten
3. Event gefeuert (timer_Elapsed)
    - Position der Schlange wird aktualisiert
	- Falls Kollision mit Schlange oder Wand -> GameOver()
	- Falls Kollision mit Süßigkeit -> Schlange wird um eine Einheit länger

============================================
================== Entities ================

Snake: Die Schlange
Candy: Für jede Süßigkeit wird die Schlange um ein Body Index erweitert => Schlange wird länger
Wall: Verbotene Zone für die Schlange

============================================
================ Neuzeichnung ==============

Snake: Positionsänderung alle 0.1s => häufige Zeichnung (wird von lvl zu lvl häufiger)
Candy: sobald eingesammelt => seltene Zeichnung
Wall: immer gleiche Stelle => einmalige Zeichnung

============================================
================== Tastatur ================

Da Konsolenanwendungen kein KeyDown Event bereitstellen, habe ich etwas Eigenes gebaut.
Die besagte Funktion läuft in einem eigenen Thread in einer Dauerschleife.
In dieser Frage ich in jedem Durchlauf ab, ob ein nicht behandelter Tastendruck vorhanden ist.
Falls ja, lies ihn und behandle ihn entsprechend.

Zunächst wird geguckt, um welche Pfeiltaste es sich handelt. Anschließend wird geprüft, ob der Richtungswechsel zulässig ist.
Nicht zulässig ist er, sobald der Richtungswechsel eine 180° Drehung darstellen würde ( z.B. links -> rechts ).

============================================
==================== Q&A ===================

Q: Wieso abonnierst du nicht einfach das KeyDown Event um Pfeiltasten zu behandeln?
A: Konsolenanwendungen stellen kein KeyDown Event zur Verfügung.