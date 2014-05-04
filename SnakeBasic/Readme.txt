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

Q: Warum gibt es eine Snake.QueuedDirection Eigenschaft?
A: Als ich diese noch nicht implementiert hatte, beobachtete ich einen Fehler, bei dem die Schlange eine 180° Drehung vollführte
   und damit in sich selbst "hineinkroch". Dadurch hat man durch die Kollisionsprüfung verloren, da der Kopf der Schlange den Körper trifft.
   Um das zu vermeiden, habe ich diese Eigenschaft implementiert.

   Warum trat der Fehler auf?
   Meine Tastaturabfrage läuft asynchron in einer Dauerschleife. Die Position der Schlange wird aber nur (Level 1) alle 100 ms geändert.
   Wenn man in diesen 100 ms mehrere Pfeiltasten geklickt hat, wurde jede so behandelt, als wäre sie die Aktuelle, soweit sie die Validierung bestanden hat,
   obwohl sie es noch nicht ist, da die Schlange noch in eine andere Richtung guckt.

   Beispiel:
   Folgendes geschieht in den 100 ms, also bewegt sich die Schlange nicht

   Blick-Schlange: oben
   Pfeiltaste rechts
   Blickrichtungsänderung zulässig? Ja (oben -> rechts = 90°)
   Blick-Schlange: rechts
   Pfeiltaste unten
   Blickrichtungsänderung zulässig? Ja (rechts -> unten = 90°)
   Blick-Schlange: unten

   100 ms zuende
   Aktuelle Blickrichtung: unten
   Schlange kriecht in sich selbst

   QueuedDirection zeigt die Richtung, die falls in diesem Moment die 100 ms um sind, auf die Schlange angewandt wird.
   Die Überprüfung, ob die Änderung nun aber zulässig ist, wird weiterhin mit der echten Schlangen-Blickrichtung geprüft.