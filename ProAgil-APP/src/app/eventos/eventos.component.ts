import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  public eventos: any = [];
  constructor(private _http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  getEventos(){

    this._http.get('http://localhost:5000/api/values').subscribe(
      (res: any) => { 
        if(res.length > 0){ 
          this.eventos = res;
          console.log(this.eventos);
        }
      },
      (err) => {
        console.error(err);
      });
  }
}
