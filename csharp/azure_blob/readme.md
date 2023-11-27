
Classe: AzureStorage

A classe AzureStorage fornece métodos para interagir com o Armazenamento de Blobs do Azure.

Propriedades:

StorageAccount: O objeto CloudStorageAccount que representa a conta de armazenamento do Azure.
BlobClient: O objeto CloudBlobClient que fornece acesso ao serviço de armazenamento de blobs.
BlobUrlContainer: O objeto CloudBlobContainer que representa o container de blobs a ser usado para armazenar arquivos.
serviceClient: O objeto BlobServiceClient que fornece acesso ao Armazenamento de Blobs do Azure.
connectionString: A string de conexão para a conta de armazenamento do Azure.
Métodos:

UploadFile(string pathFileName, string container, Stream stream): Faz upload de um arquivo para o container de blobs especificado.
DeleteFile(string pathFileName, string container): Exclui um arquivo do container de blobs especificado.
CreateServiceSASBlob(string containerName, string imgName, string storedPolicyName = null): Cria um URI SAS de nível de serviço para o blob especificado.
Método UploadFile

O método UploadFile faz upload de um arquivo para o container de blobs especificado. O método recebe os seguintes parâmetros:

pathFileName: O caminho e o nome do arquivo a ser carregado.
container: O nome do container de blobs para fazer upload do arquivo.
stream: O objeto Stream que representa o arquivo a ser carregado.
O método primeiro verifica se o container de blobs existe. Se o container não existir, ele o cria. Em seguida, ele faz upload do arquivo para o container de blobs e retorna a URL do arquivo carregado.

Método DeleteFile

O método DeleteFile exclui um arquivo do container de blobs especificado. O método recebe os seguintes parâmetros:

pathFileName: O caminho e o nome do arquivo a ser excluído.
container: O nome do container de blobs para excluir o arquivo.
O método primeiro verifica se o blob existe. Se o blob não existir, ele não faz nada. Caso contrário, ele exclui o blob.

Método CreateServiceSASBlob

O método CreateServiceSASBlob cria um URI SAS de nível de serviço para o blob especificado. O método recebe os seguintes parâmetros:

containerName: O nome do container de blobs que contém o blob.
imgName: O nome do blob para gerar o URI SAS.
storedPolicyName (opcional): O nome da política de acesso armazenada a ser usada ao gerar o URI SAS.
Se o parâmetro storedPolicyName for especificado, o método usa a política de acesso armazenada especificada para gerar o URI SAS. Caso contrário, ele gera um novo URI SAS com as permissões e o tempo de expiração especificados.

Espero que esta documentação seja útil. Informe-me se tiver outras perguntas.

Explicação adicional

A classe AzureStorage usa as bibliotecas do cliente do Armazenamento de Blobs do Azure para interagir com o serviço. A classe usa os seguintes objetos:

CloudStorageAccount: Representa a conta de armazenamento do Azure.
CloudBlobClient: Fornece acesso ao serviço de armazenamento de blobs.
CloudBlobContainer: Representa um container de blobs.
BlobClient: Representa um blob.
O método UploadFile usa os seguintes passos para fazer upload de um arquivo para um container de blobs:

Verifica se o container de blobs existe.
Se o container não existir, o cria.
Faz upload do arquivo para o container de blobs.
Retorna a URL do arquivo carregado.
O método DeleteFile usa os seguintes passos para excluir um arquivo de um container de blobs:

Verifica se o blob existe.
Se o blob existir, o exclui.
O método CreateServiceSASBlob usa os seguintes passos para criar um URI SAS de nível de serviço para um blob:

Cria um objeto BlobSasBuilder.
Define as permissões e o tempo de expiração do URI SAS.
Gera o URI SAS.
O parâmetro storedPolicyName pode ser usado para especificar uma política de acesso armazenada. Uma política de acesso armazenada é uma política de acesso que foi criada e armazenada no Azure.