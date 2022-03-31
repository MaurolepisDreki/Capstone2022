.PHONY: all clean view

all: main.pdf

view: main.pdf
	llpp $<

main.pdf: main.tex uml.png
	pdflatex -shell-escape $<

uml.png: uml.dot
	dot -Tpng:gd $< > $@

