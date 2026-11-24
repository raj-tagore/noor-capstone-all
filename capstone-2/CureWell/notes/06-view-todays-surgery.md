# Question 6 — Show today’s surgeries

View Today’s Surgery lists the surgeries the API returns for today, and each row can open the update page. This question is the list and the navigation. Question 7 is the update page.

From the method table:

- `ngOnInit` calls `getTodaySurgery()`.
- `getTodaySurgery` calls the service. On success, store the array in `surgeryList`. On failure, set `surgeryList` to `null` and store the failure in `errorMsg`. On completion, `console.log("Today's Surgery Fetched Successfully")`.
- `editSurgery(surgery)` runs from Edit Surgery Time and navigates to the update page, passing the surgery’s fields.

The service method is `getAllSurgeriesForToday`. The action is `GetAllSurgeryTypeForToday`.

`Surgery` is already declared:

```typescript
export interface Surgery {
  doctorId: number;
  endTime: number;
  startTime: number;
  surgeryCategory: string;
  surgeryDate: string;
  surgeryId: number;
}
```

Times are numbers, not clock strings. The database stores `DECIMAL(4,2)` in 24-hour `HH.MM` form: `9.00` is 09:00, `14.30` is 14:30, and the figure’s `17` and `19` are 17:00 and 19:00. Print the number you receive. Do not reformat it into `17:00` unless you mean to. The figure prints `19` and `17`.

Already used: GET, `subscribe`, the table, `(click)`, `router.navigate` with path params.

## The fetch

Same list pattern as question 1. The empty heading on this page already uses `surgeryList?.length`, so you do not need `showMsgDiv` to hide it. The error branch in the table does not mention `showMsgDiv` either. You can leave the flag false.

```typescript
getAllSurgeriesForToday(): Observable<Surgery[]> {
  return this.http
    .get<Surgery[]>(this.baseUrl + 'GetAllSurgeryTypeForToday')
    .pipe(catchError(this.errorHandler));
}
```

```typescript
ngOnInit() {
  this.getTodaySurgery();
}

getTodaySurgery() {
  this._curewellService.getAllSurgeriesForToday().subscribe({
    next: (response) => {
      this.surgeryList = response;
    },
    error: (error) => {
      this.surgeryList = null;
      this.errorMsg = error;
    },
    complete: () => {
      console.log("Today's Surgery Fetched Successfully");
    }
  });
}
```

## The date pipe

The date column is the one new piece of template syntax. A pipe reshapes a value for display and does not change the field:

```html
{{item.surgeryDate | date:"dd/MM/yyyy"}}
```

The API date looks like `2011-01-01T00:00:00`. The pipe prints `01/01/2011`, which is the figure. `item.surgeryDate` itself stays the original string. Question 7’s route must receive that original value, not the printed one.

The syntax is `value | pipeName : argument`. More than one pipe chains left to right:

```html
{{item.surgeryDate | date:"dd/MM/yyyy"}}
{{item.surgeryCategory | lowercase}}
{{item.endTime | number:"1.2-2"}}
```

| Pipe | Result |
|---|---|
| `date:"dd/MM/yyyy"` | `01/01/2011` |
| `date:"yyyy-MM-dd"` | `2011-01-01` |
| `date:"short"` | a locale short date and time |
| `uppercase` / `lowercase` | changes letters |
| `number:"1.2-2"` | at least 1 digit before the decimal, 2 after: `9.00` |
| `currency` | a money format. Not used here |

Use `date` on the date column only. Leave the times as plain interpolation so `17` stays `17`.

Other date tokens if you need a different pattern later: `dd` day, `MM` month, `yyyy` year, `HH` hour in 24-hour form, `mm` minutes. `"dd/MM/yyyy"` is the one that matches the figure.

## A route with many parameters

Edit on the doctor list passed two segments. This button passes six, in the order already written in `app.routing.ts`:

```text
editSurgery/:doctorId/:endTime/:startTime/:surgeryCategory/:surgeryDate/:surgeryId
```

End time comes before start time. That is the route, not the order of the columns in the table (the table header shows End Time and then Start Time as well).

```typescript
editSurgery(surgery: Surgery) {
  this.router.navigate([
    '/editSurgery',
    surgery.doctorId,
    surgery.endTime,
    surgery.startTime,
    surgery.surgeryCategory,
    surgery.surgeryDate,
    surgery.surgeryId
  ]);
}
```

Swap `endTime` and `startTime` in this array and the update page will show the times in the wrong boxes. The names are applied by position. Question 7 reads them back by name.

The loop variable in the shell is `item`.

```html
<tr *ngFor="let item of surgeryList" id="get_today_surgery" style="background-color:white">
  <td align="center">{{item.surgeryId}}</td>
  <td align="center">{{item.doctorId}}</td>
  <td align="center">{{item.surgeryDate | date:"dd/MM/yyyy"}}</td>
  <td align="center">{{item.endTime}}</td>
  <td align="center">{{item.startTime}}</td>
  <td align="center">{{item.surgeryCategory}}</td>
  <td>
    <button class="btn" (click)="editSurgery(item)">Edit Surgery Time</button>
  </td>
</tr>
```

`align="center"` is the same old HTML alignment the doctor table used on the table element. Here it is on each cell, matching the figure.

Open View Today’s Surgery. Each surgery is a row, the date is `dd/MM/yyyy`, and the console shows `Today's Surgery Fetched Successfully`. Edit Surgery Time on the first row changes the URL to `/editSurgery/...` with end time before start time. The update form is question 7.
