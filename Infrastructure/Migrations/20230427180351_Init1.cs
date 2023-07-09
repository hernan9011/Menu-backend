using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormaEntrega",
                columns: table => new
                {
                    FormaEntregaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaEntrega", x => x.FormaEntregaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoMercaderia",
                columns: table => new
                {
                    TipoMercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMercaderia", x => x.TipoMercaderiaId);
                });

            migrationBuilder.CreateTable(
                name: "Comanda",
                columns: table => new
                {
                    ComandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormaEntregaId = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comanda", x => x.ComandaId);
                    table.ForeignKey(
                        name: "FK_Comanda_FormaEntrega_FormaEntregaId",
                        column: x => x.FormaEntregaId,
                        principalTable: "FormaEntrega",
                        principalColumn: "FormaEntregaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mercaderia",
                columns: table => new
                {
                    MercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoMercaderiaId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Ingredientes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Preparacion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercaderia", x => x.MercaderiaId);
                    table.ForeignKey(
                        name: "FK_Mercaderia_TipoMercaderia_TipoMercaderiaId",
                        column: x => x.TipoMercaderiaId,
                        principalTable: "TipoMercaderia",
                        principalColumn: "TipoMercaderiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComandaMercaderia",
                columns: table => new
                {
                    ComandaMercaderiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MercaderiaId = table.Column<int>(type: "int", nullable: false),
                    ComandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandaMercaderia", x => x.ComandaMercaderiaId);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderia_Comanda_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comanda",
                        principalColumn: "ComandaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderia_Mercaderia_MercaderiaId",
                        column: x => x.MercaderiaId,
                        principalTable: "Mercaderia",
                        principalColumn: "MercaderiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FormaEntrega",
                columns: new[] { "FormaEntregaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "SALON" },
                    { 2, "DELIVERY" },
                    { 3, "PEDIDOS YA" }
                });

            migrationBuilder.InsertData(
                table: "TipoMercaderia",
                columns: new[] { "TipoMercaderiaId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "ENTRADA" },
                    { 2, "MINUTAS" },
                    { 3, "PASTAS" },
                    { 4, "PARRILLA" },
                    { 5, "Pizzas" },
                    { 6, "SANDWICH" },
                    { 7, "ENSALADAS" },
                    { 8, "BEBIDAS" },
                    { 9, "CERVEZA ARTESANAL" },
                    { 10, "POSTRES" }
                });

            migrationBuilder.InsertData(
                table: "Mercaderia",
                columns: new[] { "MercaderiaId", "Imagen", "Ingredientes", "Nombre", "Precio", "Preparacion", "TipoMercaderiaId" },
                values: new object[,]
                {
                    { 1, "https://www.comedera.com/wp-content/uploads/2020/12/sandwich-1580348_1280.jpg", " 4 rebanadas de pan de molde sin corteza,150 g. de atún natural en conserva,1/4 de cebolla roja cortada en cuadritos,2 cucharadas de mayonesa,Sal y pimienta al gusto", "SANDWICH DE ATUN", 1500, "En un bol agregamos el atún, la cebolla roja, la mayonesa, un poco de sal, un poco de pimienta y mezclamos todo muy bien hasta integrar.Agregamos relleno en las rebanadas de pan y luego le colocamos la otra rebanada encima.", 6 },
                    { 2, "https://www.comedera.com/wp-content/uploads/2021/01/sandwich-de-huevo.jpg", "1 pieza de pan baguette de unos 30 centímetros de largo,2 huevos,1/2 to mate,½ aguacate,4 lonjas de tocineta,2 lonchas de queso cheddar,Mayonesa al gusto,Sal al gusto", "SANDWICH DE HUEVO", 2500, "Abre la pieza de baguette por la mitad y caliéntala en una plancha a fuego suave.Cocina los huevos en omelette de forma tradicional, en una sartén no adherente y Fríe la tocineta en una sartén.", 6 },
                    { 3, "https://www.comedera.com/wp-content/uploads/2022/11/sandwich-de-salchichon.jpg", "1 pieza de pan fresco,2 rebanadas de queso blanco,8 lonchas de salchichón,Mayonesa al gusto,Pimienta al gusto", "SANDWICH DE SALCHICHON", 3000, "Coloque el pan en una bandeja para hornear durante 8-10 minutos. Retire el pan del horno y deje que se enfríe. Coloque el queso en rodajas , pepperoni , pimienta y mayonesa .", 6 },
                    { 4, "https://hoycomemossano.com/wp-content/uploads/2019/02/Bocadillo-salmon-queso-y-avo-I.jpg", "300 gr. de salmón noruego ahumado,50 gr. de harina de trigo,Queso crema,Pan de cereales, 2 cucharadas de mayonesa,1/2 cucharadita de eneldo,Ralladura de limón,1/2 limón,Rúcula ", "BOCATA DE SALMON", 1330, "en un recipiente agregamos la mayonesa, el eneldo, la ralladura de limón, el jugo de limón y mezclamos tostar las rebanadas del pan,le untamos queso crema, luego a la otra rebanada le untamos la mayonesa.Agregamos el salmón, la rúcula y ya.", 6 },
                    { 5, "https://www.comedera.com/wp-content/uploads/2020/12/delicious-roasted-young-potatoes-with-dill-top-view-1536x1024.jpg", "1 kilo de papas cambray,½ cebolla entera,3 dientes de ajo,1 lata de chile chipotle,5 chiles guajillos,Sal al gusto,Pimienta al gusto,½ taza de agua,½ cucharada de achiote", "PAPAS ADOBADAS", 500, " Asa los chiles guajillo con el ajo y la cebolla entera, hirviendo los chiles por 20 minutos,Licúa todo, menos las papas,Cuela la salsa,Hierve las papas con sal por 10 minutos,Mezcla las papas con el adobo y marina por una hora, hornea por 15", 2 },
                    { 6, "https://www.comedera.com/wp-content/uploads/2016/06/pasta-5579058_1280.jpg", "2 porciones de fettuccine también le dicen fettuccini,2 cucharadas de mantequilla,½ taza de queso parmesano rallad,2 cucharadas de mantequilla,1 taza de nata,½ taza de queso parmesano rallado,2 dientes de ajo picados muy pequeño,Pimienta ", "PASTA ALFREDO", 3100, "Cocina la pasta, le agregas la mantequilla y el queso .Revuelve, agrega pimienta y listo.Caliente una sartén con aceite y fríe los ajos, agrega la mantequilla y la nata.Agrega el queso parmesano. Mezcla .Ponle la mescla a tu pasta favorita", 3 },
                    { 7, "https://www.comedera.com/wp-content/uploads/2021/01/pizza-4242967_1280.jpg", "400 gramos harina,200 ml agua tibia,2 cucharadas aceite de oliva,15 gramos levadura fresca,pizca de sal,1 taza salsa de jitomate,1 taza queso mozzarella,¼ taza queso parmesano rallado,Pepperoni o jamón al gusto", "MINI PIZZAS", 2100, "En un bowl agrega el aceite, el agua y la levadura.Mezcla y añade la harina ,sal y amasa 5 minutos .reposar tu masa una hora. haz pequeñas tortillitas circulares.Coloca salsa de jitomate, mozzarella ,parmesano y pepperoni .Hornea de15 minutos", 5 },
                    { 8, "https://www.comedera.com/wp-content/uploads/2023/03/sopa-de-pescado-con-arroz.jpg", "1/2 kg de filetes de pescado blanco merluza o lenguado,1 cebolla,2 dientes de ajo,1 pimiento rojo ,1 papa grande ,1 taza de arroz blanco,perejil fresco ,4 tazas de caldo de pescado,2 cucharadas de aceite de oliva,Sal al gusto,Pimienta negra ", "SOPA DE PESCADO CON ARROZ", 1900, "Calienta olla con aceite . Añade la cebolla, el ajo y el pimiento rojo y cocina por 5 minutos, .Añade las papas, el arroz y cocinar a fuego lento por 15 minutos. luego los filetes de pescado y cocinar 5 minutos .Añade sal y pimienta, y el perejil .", 1 },
                    { 9, "https://www.comedera.com/wp-content/uploads/2023/02/Mimosa-paso-3-PG_BCC200120006-300x169.jpg", "½ litro de champaña fría,½ litro de zumo de naranja natural,rodajas de naranja para decorar", "MIMOSA", 900, "Debes usar una copa larga, sirve la mitad de champaña.Completa la copa con jugo de naranja, es decir, son partes iguales de champaña y de jugo.Decora con frambuesas congeladas y una rodaja de naranja. Disfruta.", 8 },
                    { 10, "https://www.comedera.com/wp-content/uploads/2023/02/coctel-con-champagne-paso-4-PG_BCC200120010-300x169.jpg", "3 cdas. de puré de fresa fresco,½ cdta. de jugo de limón,Champaña,Fresa cortada en rodajas para decorar,Corteza de limón", "COCTEL DE CHAMPAÑA, FRESAS Y LIMÓN", 900, "Mezcla el puré de fresas con el limón.En una copa larga añade la mezcla .Rellena el resto de la copa con champaña.Decora con una rebanada de fresa y con corteza de limón. Disfruta.", 8 },
                    { 11, "https://cdn7.kiwilimon.com/recetaimagen/12746/18280.jpg", "2 onzas de tequila,1 limón,1/2 cda de sal (para escarchar el vaso),1 caso de refresco de toronja,Hielos en cubos", "PALOMA", 900, "Toma tu vaso, lo humedecerás col limón en el borde y los vas a escarchar con la sal.Ahora, en la coctelera agregarás el hilo , el tequila, una pizca de sal y un poco de limón,mezclar y servirás en el vaso con la toronja.", 8 },
                    { 12, "https://www.recetasderechupete.com/wp-content/uploads/2022/11/Tequila-Sunrise.jpg", "3 onzas de tequila,4 naranjas,3 onzas de granadina,2 rodajas de limón,1 cereza", "SUNRISE", 1000, "En una coctelera, añadirás el hielo, el tequila y el jugo de naranja. Mezcla y añadirás la granadina y un poco de limón.añadir la mezcla del resto de los ingredientes para formar tu cóctel.Decora con las rodajas de limón y la cereza.", 8 },
                    { 13, "https://www.comedera.com/wp-content/uploads/2023/04/Martini-de-mazapan-PG_MDM180520002.jpg", "1 mazapán,3 hielos,4 oz de leche evaporada,¾ oz de licor de avellana,½ oz de licor de café,1 ½ oz de vodka,Azúcar con canela,Chocolate líquido", "MARTINI DE MAZAPAN", 800, "Escarcha la copa con chocolate líquido.Pasa la copa por la mezcla de azúcar con canela.Coloca hielos, la leche , el licor de avellana, 1 pieza de mazapán, licor de café y vodka.Agita.Sirve en la copa y decora con chocolate líquido. ", 8 },
                    { 14, "https://www.comedera.com/wp-content/uploads/2022/08/Choripan-con-chimichurri-PG_CHCCH191021_01.jpg", "1 libra chorizo argentino,2 baguettes,¼ taza agua,6 cdas. vinagre de vino tinto,6 dientes de ajo,1 hoja de laurel,½ cdta. hojuelas de chile seco,1 cda. orégano,1 cda. pimienta,½ taza aceite de oliva,1 taza perejil rizado", "CHORIPAN CON CHIMICHURRI", 500, "En un bowl agrega el agua, el vinagre, el ajo, el laurel el chile seco,perejil, el orégano, la pimienta y aceite . Mezcla bien.Para el choripán, chorizos en el grill durante 2 minutos.Corta los panes, rellena con el chorizo y chimichurri ", 4 },
                    { 15, "https://www.comedera.com/wp-content/uploads/2022/08/brocheta-de-carne-con-verduras.jpeg", "600 gr de carne (cortada para brochetas)1 pimiento rojo,1 cebolla morada,2 calabacitas,Sal, pimienta y ajo en polvo al gusto,Salsa sazonadora o barbecue,2 cdas de cilantro fresco picado.Aceite de oliva", "BROCHETAS DE CARNE CON VERDURAS", 700, "Añade la carne en un bol, y agrega el jugo de limón, el cilantro picado, el ajo en polvo, la sal y la pimienta. Mezcla .sazonada tu carne,dejarás marinar. coloca un trozo de carne, luego las verduras en el palillo.en sartén con aceite y Cocina", 4 },
                    { 16, "https://www.comedera.com/wp-content/uploads/2022/08/Chuzos-ecuatorianos-shutterstock_2160614305.jpg", "1 kg. lomo de asado limpiozos,3 cdas. aceite con achiote,4 dientes de ajo rallados,1 cda. comino molido,1 cdta. pimienta molida,½ cda. sal,Palitos de madera para chuzos Humedecidos 40 minutos antes de armarlos", "CHUZOS ECUATORIANOS", 800, "Dentro de un bol añade el ajo, el comino, la sal, el aceite y la pimienta. Mezcla.Agrega la carne y deja marinar por 2 horas.Toma la carne, y ve insertándola en el palito, Sobre la parrilla coloca los chuzos y deja cocinar por 5 minutos ", 4 },
                    { 17, "https://elmundoenrecetas.s3.amazonaws.com/uploads/recipe/main_image/63/IMG-3268_1200_px__2_.jpg", "24 alitas de pollo, 150 ml. de salsa de tomate natural,3 cucharadas de salsa de soja,2 cucharadas de vinagre de arroz, 1 cucharada de papelón o panela molida,1 cucharada de sriracha,Semillas de ajonjolí,Sal al gusto,Pimienta al gusto", "ALITAS DE POLLO ESTILO COREANO", 1200, "En una olla a fuego con tomate natural, la salsa de soja, el vinagre de arroz, el papelón o panela molida, la sriracha y mezcla. agraga un poco de sal y pimienta, y cocinar por 25 minutos.agrega salsa oriental y cocinar por 10 minutos ", 4 },
                    { 18, "https://elmundoenrecetas.s3.amazonaws.com/uploads/recipe/main_image/491/arroz_con_leche_y_chocolate.webp", "100 g. de arroz arborio o arroz bomba,900 ml. de leche entera,1 rama de canela,70 g. de azúcar,1 cucharada de vainilla, 70 g. de chocolate puro,Chocolate rallado a gusto para decorar", "ARROZ CON LECHE Y CHOCOLATE", 1800, "En un cazo, agregamos la leche, la rama de canela, la vainilla y mezclamos, agregamos el arroz y cocinar por 45 minutos .Agregamos el azúcar, removemos y dejamos cocinar por 10 minutos, agregamos el chocolate fundido a nuestro arroz con leche y mescla.", 10 },
                    { 19, "https://elmundoenrecetas.s3.amazonaws.com/uploads/recipe/main_image/191/IMG_8913-1200.jpg", "250 g. de ricota,150 g. de harina,60 g. de azúcar,1 huevo,Ralladura de limón,6 g. de polvo de hornear,Azúcar Glass,Leche condensada,Aceite para freír", "CASTAÑAS DE RICOTA", 900, "En un recipiente colocar la ricota, el huevo, el azúcar, y mezclamos todo, agregamos la ralladura de limón, la harina y mezclamos . formar las bolitas, las freímos hasta dorar, las sacamos y Econ el agregamos azúcar glass y leche condensada ", 10 },
                    { 20, "https://elmundoenrecetas.s3.amazonaws.com/uploads/recipe/main_image/261/IMG_1675-1280.jpg", "2 plátanos cortados en rodajas,2 huevos,150 g. de azúcar,225 g. de harina de trigo todo uso,1 y 1/2 cucharadita de levadura química o polvo de hornear,80 g. de chocolate,1 cucharadita de canela en polvo,100 ml. de aceite vegetal", "BIZCOCHO DE PLÁTANO Y CHOCOLATE", 850, "trituramos nuestro plátano y en un recipiente colocar el aceite, el azúcar y con la ayuda de unas varillas, mezclar, agregamos los huevos y el plátano mezclar.agregamos la levadura, la canela en polvo, el chocolate  y horno por 45 minutos", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_FormaEntregaId",
                table: "Comanda",
                column: "FormaEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderia_ComandaId",
                table: "ComandaMercaderia",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderia_MercaderiaId",
                table: "ComandaMercaderia",
                column: "MercaderiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mercaderia_TipoMercaderiaId",
                table: "Mercaderia",
                column: "TipoMercaderiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComandaMercaderia");

            migrationBuilder.DropTable(
                name: "Comanda");

            migrationBuilder.DropTable(
                name: "Mercaderia");

            migrationBuilder.DropTable(
                name: "FormaEntrega");

            migrationBuilder.DropTable(
                name: "TipoMercaderia");
        }
    }
}
