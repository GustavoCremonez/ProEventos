import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent {
  public eventos: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {
    this.http.get('https://localhost:44391/api/Eventos').subscribe({
      next: this.handleUpdateEventos.bind(this),
      error: this.handleError.bind(this),
    });
  }

  public handleUpdateEventos(response: any): void {
    this.eventos = response;
  }

  public handleError(error: Error): void {
    console.error(error);
  }
}
