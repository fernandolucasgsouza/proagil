import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  public eventos: any = [];
  public imageWidth = 70;
  public imageMargin = 2;
  public imageTable = true;

  public _filtroLista: string;
  public get filtroLista(): string {
    return this._filtroLista;
  }
  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventoFiltrados = this.filtroLista
      ? this.filtrarEventos(this.filtroLista)
      : this.eventos;
  }

  public eventoFiltrados: any = [];

  constructor(private _http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  getEventos() {
    this._http.get('http://localhost:5000/api/values').subscribe(
      (res: any) => {
        if (res.length > 0) {
          this.eventoFiltrados = [...this.eventos] = res;
        }
      },
      err => {
        console.error(err);
      }
    );
  }

  toogleImage() {
    this.imageTable = !this.imageTable;
  }

  filtrarEventos(filtraPor: string): any {
    filtraPor = filtraPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtraPor) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filtraPor) !== -1
    );
  }
}