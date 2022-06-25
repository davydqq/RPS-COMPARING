import { Inject, Injectable } from '@nestjs/common';
import { PG_CONNECTION } from 'src/components/db/db.module';

@Injectable()
export class FilmsService {
  constructor(@Inject(PG_CONNECTION) private conn: any) {}

  async getFilms(count?: number) {
    let query = 'SELECT * FROM film';

    if (count && count > 0) {
      query = `SELECT * FROM film LIMIT ${count}`;
    }

    const res = await this.conn.query(query);
    return res.rows;
  }
}
